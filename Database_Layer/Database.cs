using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace Database_Layer
{
    public static class Database
    {
        private static string host = "localhost";
        private static string username = "proftaak";
        private static string password = "proftaak";
        private static string connectionstring = "User Id=proftaak;Password=proftaak;Data Source=localhost:1521/XE;"; //"User Id=" + username + ";Password=" + password + ";Data Source= //" + host + ":1521/XE;";

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

        public static int ReturnIdLastInsertID(string seqName)
        {
            string query = "SELECT " + seqName + ".CURRVAL FROM dual";
            int ID = 0;

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
                                ID = Convert.ToInt32(reader[0]);
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
            return ID;
        }
    }
}
