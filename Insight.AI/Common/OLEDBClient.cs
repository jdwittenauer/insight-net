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
using System.Data.OleDb;

namespace Insight.AI.Common
{
    /// <summary>
    /// This static class exposes methods for connecting to OLEDB data sources.
    /// </summary>
    public static class OLEDBClient
    {
        /// <summary>
        /// Executes an inline SQL statement and returns the number of rows affected.
        /// </summary>
        /// <param name="dbStatement">Inline SQL</param>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Integer indicating the number of rows affected by the query</returns>
        public static int RunStatement(string dbStatement, string connectionString)
        {
            OleDbConnection dbConnection = null;
            OleDbCommand dbCommand = null;

            try
            {
                dbConnection = new OleDbConnection(connectionString);
                dbCommand = new OleDbCommand(dbStatement, dbConnection);
                dbCommand.CommandType = CommandType.Text;

                dbConnection.Open();
                return dbCommand.ExecuteNonQuery();
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConnection != null)
                    dbConnection.Dispose();
            }
        }

        /// <summary>
        /// Executes an inline SQL statement and returns the number of rows affected.
        /// </summary>
        /// <param name="dbStatement">Inline SQL</param>
        /// <param name="connectionString">Connection string</param>
        /// <param name="commandParameters">Parameters for the SQL statement</param>
        /// <returns>Integer indicating the number of rows affected by the query</returns>
        public static int RunStatement(string dbStatement, string connectionString,
            params OleDbParameter[] commandParameters)
        {
            OleDbConnection dbConnection = null;
            OleDbCommand dbCommand = null;

            try
            {
                dbConnection = new OleDbConnection(connectionString);
                dbCommand = new OleDbCommand(dbStatement, dbConnection);
                dbCommand.CommandType = CommandType.Text;

                if (commandParameters != null)
                {
                    for (int i = 0; i < commandParameters.Length; i++)
                    {
                        if (commandParameters[i] != null)
                        {
                            dbCommand.Parameters.Add(commandParameters[i]);
                        }
                    }
                }

                dbConnection.Open();
                return dbCommand.ExecuteNonQuery();
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConnection != null)
                    dbConnection.Dispose();
            }
        }

        /// <summary>
        /// Executes an inline SQL statement and returns a data table.
        /// </summary>
        /// <param name="dbStatement">Inline SQL</param>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Data table containing the return data</returns>
        public static DataTable RunQuery(string dbStatement, string connectionString)
        {
            OleDbConnection dbConnection = null;
            OleDbCommand dbCommand = null;
            OleDbDataAdapter adapter = null;
            DataTable dt = null;

            try
            {
                dbConnection = new OleDbConnection(connectionString);
                dbCommand = new OleDbCommand(dbStatement, dbConnection);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandTimeout = 600;

                adapter = new OleDbDataAdapter(dbCommand);
                dt = new DataTable();

                dbConnection.Open();
                adapter.Fill(dt);
                return dt;
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConnection != null)
                    dbConnection.Dispose();
            }
        }

        /// <summary>
        /// Executes an inline SQL statement and returns a data table.
        /// </summary>
        /// <param name="dbStatement">Inline SQL</param>
        /// <param name="connectionString">Connection string</param>
        /// <param name="commandParameters">Parameters for the SQL statement</param>
        /// <returns>Data table containing the return data</returns>
        public static DataTable RunQuery(string dbStatement, string connectionString,
            params OleDbParameter[] commandParameters)
        {
            OleDbConnection dbConnection = null;
            OleDbCommand dbCommand = null;
            OleDbDataAdapter adapter = null;
            DataTable dt = null;

            try
            {
                dbConnection = new OleDbConnection(connectionString);
                dbCommand = new OleDbCommand(dbStatement, dbConnection);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandTimeout = 600;

                if (commandParameters != null)
                {
                    for (int i = 0; i < commandParameters.Length; i++)
                    {
                        if (commandParameters[i] != null)
                        {
                            dbCommand.Parameters.Add(commandParameters[i]);
                        }
                    }
                }

                adapter = new OleDbDataAdapter(dbCommand);
                dt = new DataTable();

                dbConnection.Open();
                adapter.Fill(dt);
                return dt;
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConnection != null)
                    dbConnection.Dispose();
            }
        }
    }
}
