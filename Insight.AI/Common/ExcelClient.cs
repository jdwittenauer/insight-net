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
using System.Data;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Insight.AI.Common
{
    public static class ExcelClient
    {
        /// <summary>
        /// Parses data from an Excel spreadsheet into a data table.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="sheetName">Sheet name</param>
        /// <param name="firstRowAsNames">Indicates if the first row contains atribute names</param>
        /// <returns>Data table containing the parsed data</returns>
        public static DataTable ParseExcelFile(string path, string sheetName, bool firstRowAsNames)
        {
            DataTable table = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(path, false))
            {
                spreadSheetDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = false;
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook
                    .GetFirstChild<Sheets>()
                    .Elements<Sheet>();
                string relationshipId = sheets.Where(x => x.Name == sheetName).First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart
                    .GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                int i = 0;
                foreach (Cell cell in rows.ElementAt(0))
                {
                    table.Columns.Add(i.ToString());
                    i++;
                }

                foreach (Row row in rows)
                {
                    DataRow tempRow = table.NewRow();

                    for (int j = 0; j < row.Descendants<Cell>().Count(); j++)
                    {
                        tempRow[j] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(j));
                    }

                    table.Rows.Add(tempRow);
                }
            }

            return table;
        }

        /// <summary>
        /// Helper method that parses out a cell's value
        /// </summary>
        /// <param name="document">Open XML spreadhsheet document</param>
        /// <param name="cell">Cell</param>
        /// <returns>String representing the cell's value</returns>
        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            if (cell.CellValue != null)
            {
                SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
                string value = cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
