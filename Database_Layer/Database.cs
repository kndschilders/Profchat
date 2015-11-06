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

        public static DataTable RetrieveQuery(string select, string from, string where)
        {
            using (OracleConnection c = new OracleConnection(@connectionstring))
            {
                c.Open();
                OracleCommand cmd = new OracleCommand("SELECT :x FROM :y WHERE :z");
                cmd.Parameters.Add(new OracleParameter("x", select));
                cmd.Parameters.Add(new OracleParameter("y", from));
                cmd.Parameters.Add(new OracleParameter("z", where));
                cmd.Connection = c;
                try
                {
                    OracleDataReader r = cmd.ExecuteReader();
                    DataTable result = new DataTable();
                    result.Load(r);
                    c.Close();
                    return result;
                }
                catch (Exception e)
                {
                    string debug = e.Message;
                    throw;
                }
            }
        }

        public static void ExecuteQuery(string query)
        {
            using (OracleConnection c = new OracleConnection(@connectionstring))
            {
                c.Open();
                OracleCommand cmd = new OracleCommand(":qq");
                cmd.Parameters.Add(new OracleParameter("qq", query));
                cmd.Connection = c;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                c.Close();
            }
        }

        public static void UpdateQuery(string query)
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
    }
}
