using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Layer;
using Oracle.DataAccess.Client;
using System.Data;

namespace Class_Layer
{
    public class Data
    {
        public List<Gebruiker> HaalGebruikersOp()
        {
            List<Gebruiker> returned = new List<Gebruiker>();

            string query = "SELECT ID, NAAM, ISONLINE FROM \"USER\"";

            DataTable dt = Database.RetrieveQuery(query);

            foreach (DataRow d in dt.Rows)
            {
                int id = Convert.ToInt32(d["ID"]);
                string naam = Convert.ToString(d["NAAM"]);
                int isOnline = Convert.ToInt32(d["ISONLINE"]);

                returned.Add(new Gebruiker(id, naam, isOnline));
            }

            return returned;
        }

        public bool UpdateOnlineStatus(bool newStatus, int gebrID)
        {
            bool gelukt = false;
            string query = string.Empty;

            if (newStatus)
                query = "UPDATE \"USER\" SET ISONLINE = 1 WHERE ID = " + gebrID;
            else
                query = "UPDATE \"USER\" SET ISONLINE = 0 WHERE ID = " + gebrID;
            
            try
            {
                Database.UpdateQuery(query);

                gelukt = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return gelukt;
        }
    }
}
