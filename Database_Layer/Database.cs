//-----------------------------------------------------------------------
// <copyright file="Database.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Database_Layer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// The database class is used to communicate to the database.
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// The connection string of the chat server
        /// </summary>
        private static string connectionstring = "User Id=proftaak;Password=proftaak;Data Source=localhost:1521/XE;";

        /// <summary>
        /// The connection string of the main database, used to retrieve the user on initialization
        /// </summary>
        private static string connectionstring2 = "User Id=PLUMBUM;Password=root;Data Source= //192.168.20.27:1521/XE;";

        /// <summary>
        /// Use only to retrieve user on initialization
        /// </summary>
        /// <param name="query">The input query</param>
        /// <returns>A DataTable containing the results of the query</returns>
        public static DataTable HaalGebruikerOp(string query)
        {
            using (OracleConnection conn = new OracleConnection(connectionstring2))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(query);
                    cmd.Connection = conn;
                    try
                    {
                        OracleDataReader reader = cmd.ExecuteReader();
                        DataTable result = new DataTable();
                        result.Load(reader);
                        conn.Close();
                        return result;
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                    return new DataTable();
                }
            }
        }

        /// <summary>
        /// Retrieve information from the database
        /// </summary>
        /// <param name="query">The input query</param>
        /// <returns>A DataTable containing the results of the query</returns>
        public static DataTable RetrieveQuery(string query)
        {
            using (OracleConnection c = new OracleConnection(@connectionstring))
            {
                try
                {
                    c.Open();
                    OracleCommand cmd = new OracleCommand(@query);
                    cmd.Connection = c;
                    try
                    {
                        OracleDataReader r = cmd.ExecuteReader();
                        DataTable result = new DataTable();
                        result.Load(r);
                        c.Close();
                        return result;
                    }
                    catch (OracleException e)
                    {
                        Console.Write(e.Message);
                        throw;
                    }
                }
                catch (OracleException e)
                {
                    Console.Write(e.Message);
                    return new DataTable();
                }
            }
        }

        /// <summary>
        /// Execute a query on the server with no return value, like an INSERT or DELETE query
        /// </summary>
        /// <param name="query">The input query</param>
        public static void ExecuteQuery(string query)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionstring))
                {
                    conn.Open();
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Returns the current value of a given sequence
        /// </summary>
        /// <param name="seqName">The sequence name you want to get the last value from</param>
        /// <returns>The last value of the given sequence name</returns>
        public static int ReturnIdLastInsertID(string seqName)
        {
            string query = "SELECT " + seqName + ".CURRVAL FROM dual";
            int id = 0;

            using (OracleConnection conn = new OracleConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(query);
                    cmd.Connection = conn;
                    try
                    {
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                id = Convert.ToInt32(reader[0]);
                            }
                        }

                        conn.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                catch (OracleException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return id;
        }
    }
}
