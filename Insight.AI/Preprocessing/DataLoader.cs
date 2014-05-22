// Copyright (c) 2013 John Wittenauer (Insight.NET)

// This file is part of Insight.NET.

// Insight.NET is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Insight.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with Insight.NET.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Insight.AI.DataStructures;
using Insight.AI.Preprocessing.Common;

namespace Insight.AI.Preprocessing
{
    public static class DataLoader
    {
        /// <summary>
        /// Imports data from a file with comma-separated values.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="separator">Separator character</param>
        /// <param name="firstRowAsNames">Indicates if the first row contains atribute names</param>
        /// <param name="labelColumn">Indicates the column of the class label or prediction target</param>
        /// <param name="convertNominalLabel">Convert a nominal label to an integer</param>
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromCSV(string path, char separator, bool firstRowAsNames,
            int? labelColumn, bool? convertNominalLabel)
        {
            var table = CSVClient.ParseCSVFile(path, separator, firstRowAsNames);

            // For now we're ignoring text such as column names -
            // need a better data structure to keep track of text values
            var matrix = ParseTableIntoNumericList(table, labelColumn, convertNominalLabel);

            if (labelColumn != null)
            {
                matrix.Label = labelColumn.Value;
            }

            return matrix;
        }

        /// <summary>
        /// Imports data from an Excel spreadsheet.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="sheetName">Sheet name</param>
        /// <param name="firstRowAsNames">Indicates if the first row contains atribute names</param>
        /// <param name="labelColumn">Indicates the column of the class label or prediction target</param>
        /// <param name="convertNominalLabel">Convert a nominal label to an integer</param>
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromExcel(string path, string sheetName, bool firstRowAsNames,
            int? labelColumn, bool? convertNominalLabel)
        {
            var table = ExcelClient.ParseExcelFile(path, sheetName, firstRowAsNames);

            // For now we're ignoring text such as column names -
            // need a better data structure to keep track of text values
            var matrix = ParseTableIntoNumericList(table, labelColumn, convertNominalLabel);

            if (labelColumn != null)
            {
                matrix.Label = labelColumn.Value;
            }

            return matrix;
        }

        /// <summary>
        /// Imports data from a database.
        /// </summary>
        /// <param name="connection">Connection string</param>
        /// <param name="connectionType">Conection type</param>
        /// <param name="query">SQL statement to that produces result set to import</param>
        /// <param name="labelColumn">Indicates the column of the class label or prediction target</param>
        /// <param name="convertNominalLabel">Convert a nominal label to an integer</param>
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromDatabase(string connection, string connectionType,
            string query, int? labelColumn, bool? convertNominalLabel)
        {
            DataTable table;
            if (connectionType == "MS SQL")
            {
                table = SQLClient.RunQuery(connection, query);
            }
            else
            {
                table = OLEDBClient.RunQuery(connection, query);
            }

            // For now we're ignoring text such as column names -
            // need a better data structure to keep track of text values
            var matrix = ParseTableIntoNumericList(table, labelColumn, convertNominalLabel);

            if (labelColumn != null)
            {
                matrix.Label = labelColumn.Value;
            }

            return matrix;
        }

        /// <summary>
        /// Parses a data table into a numeric matrix.
        /// </summary>
        /// <param name="table">Data table</param>
        /// <param name="labelColumn">Indicates the column of the class label or prediction target</param>
        /// <param name="convertNominalLabel">Convert a nominal label to an integer</param>
        /// <returns>Numeric matrix</returns>
        private static InsightMatrix ParseTableIntoNumericList(DataTable table, 
            int? labelColumn, bool? convertNominalLabel)
        {
            int rowCount = table.Rows.Count, columnCount = table.Columns.Count;
            var columnIsNumeric = new List<bool>();

            for (int i = 0; i < columnCount; i++)
            {
                // Need to determine if the column is numeric
                bool isNumeric = true;
                for (int j = 0; j < 10; j++)
                {
                    if (j < rowCount)
                    {
                        double value;
                        if (table.Rows[j][i] != null && !double.TryParse(table.Rows[j][i].ToString(), out value))
                        {
                            // Couldn't parse value into a number
                            isNumeric = false;
                        }
                    }
                }

                columnIsNumeric.Add(isNumeric);
            }

            var numericColumnCount = columnIsNumeric.Where(x => x == true).Count();

            if (labelColumn.HasValue && convertNominalLabel == true && !columnIsNumeric[labelColumn.Value])
            {
                // Have a label that isn't numeric but will be converted to integer and included in the data
                numericColumnCount++;
            }

            var data = new List<List<double>>();
            var labels = new Dictionary<string, int>();

            for (int i = 0; i < rowCount; i++)
            {
                var row = new List<double>();

                for (int j = 0; j < columnCount; j++)
                {
                    // If the column isn't numeric then skip it - eventually we'll need a
                    // more advanced data structure than can deal with text data
                    if (columnIsNumeric[j])
                    {
                        double value;
                        if (double.TryParse(table.Rows[i][j].ToString(), out value))
                        {
                            row.Add(value);
                        }
                        else
                        {
                            // If the value can't be parsed then just insert a zero - this is a very 
                            // naive approach but will work as a starting point
                            row.Add(0);
                        }
                    }
                    else if (labelColumn.HasValue && labelColumn == j && 
                        convertNominalLabel.HasValue && convertNominalLabel == true)
                    {
                        // Label column, values are text but we need to convert to int
                        string value = table.Rows[i][j].ToString();

                        if (!labels.ContainsKey(value))
                        {
                            labels.Add(value, labels.Count);
                        }

                        row.Add(Convert.ToDouble(labels[value]));
                    }
                }

                data.Add(row);
            }

            return new InsightMatrix(rowCount, numericColumnCount, data);
        }
    }
}
