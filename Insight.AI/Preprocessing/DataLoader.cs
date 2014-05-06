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
using System.Linq;
using System.Text;
using Insight.AI.DataStructures;

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
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromCSV(string path, char separator, bool firstRowAsNames)
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports data from an Excel spreadsheet.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="sheetName">Sheet name</param>
        /// <param name="firstRowAsNames">Indicates if the first row contains atribute names</param>
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromExcel(string path, string sheetName, bool firstRowAsNames)
        {
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports data from a database.
        /// </summary>
        /// <param name="connection">Connection string</param>
        /// <param name="connectionType">Conection type</param>
        /// <param name="query">SQL statement to that produces result set to import</param>
        /// <returns>Matrix instantiated with the imported data</returns>
        public static InsightMatrix ImportFromDatabase(string connection, string connectionType, string query)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
