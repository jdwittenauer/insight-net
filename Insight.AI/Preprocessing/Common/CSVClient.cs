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

using System.Data;
using System.IO;
using System.Linq;

namespace Insight.AI.Preprocessing.Common
{
    public static class CSVClient
    {
        /// <summary>
        /// Parses data from a CSV file into a data table.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="separator">Separator character</param>
        /// <param name="firstRowAsNames">Indicates if the first row contains atribute names</param>
        /// <returns>Data table containing the parsed data</returns>
        public static DataTable ParseCSVFile(string path, char separator, bool firstRowAsNames)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var table = new DataTable();
                var headerTokens = reader.ReadLine().Split(separator).ToList();

                if (firstRowAsNames)
                {
                    for (int i = 0; i < headerTokens.Count; i++)
                    {
                        table.Columns.Add(headerTokens[i].Trim());
                    }
                }
                else
                {
                    for (int i = 0; i < headerTokens.Count; i++)
                    {
                        table.Columns.Add();
                    }

                    DataRow row = table.NewRow();

                    for (int i = 0; i < headerTokens.Count; i++)
                    {
                        row[i] = headerTokens[i].Trim();
                    }

                    table.Rows.Add(row);
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line.Length > 0 && line.Contains(separator))
                    {
                        var tokens = line.Split(separator).ToList();
                        DataRow row = table.NewRow();

                        for (int i = 0; i < tokens.Count; i++)
                        {
                            row[i] = tokens[i].Trim();
                        }

                        table.Rows.Add(row);
                    }
                }

                return table;
            }
        }
    }
}
