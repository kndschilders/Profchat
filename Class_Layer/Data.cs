using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Layer;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;

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

        public Gebruiker HaalGebruikerOp(int gebruikerID)
        {
            string query = "SELECT ID, NAAM, ISONLINE FROM \"USER\" WHERE ID = " + gebruikerID;

            DataTable dt = Database.RetrieveQuery(query);


            //foreach (DataRow d in dt.Rows)
            //{
            //    int id = Convert.ToInt32(d["ID"]);
            //    string naam = Convert.ToString(d["NAAM"]);
            //    int isOnline = Convert.ToInt32(d["ISONLINE"]);

            //    returned.Add(new Gebruiker(id, naam, isOnline));
            //}

            int id = Convert.ToInt32(dt.Rows[0]["ID"]);
            string naam = Convert.ToString(dt.Rows[0]["NAAM"]);
            int isOnline = Convert.ToInt32(dt.Rows[0]["ISONLINE"]);

            return new Gebruiker(id, naam, isOnline);
        }

        public bool UpdateOnlineStatus(bool newStatus, int gebrID)
        {
            string query = string.Empty;

            if (newStatus)
                query = "UPDATE \"USER\" SET ISONLINE = 1 WHERE ID = " + gebrID;
            else
                query = "UPDATE \"USER\" SET ISONLINE = 0 WHERE ID = " + gebrID;
            
            try
            {
                Database.ExecuteQuery(query);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public void SendMessage(string message, int gebrID, int chatroomID)
        {
            string query = "INSERT INTO MESSAGES VALUES(MESSAGES_SEQ.NEXTVAL, " + gebrID + ", " + chatroomID + ", " + "'" + message + "'" + ", CURRENT_TIMESTAMP)";

            Console.WriteLine(query);

            Database.ExecuteQuery(query);
        }

        public DataTable ReturnMessages(int chatroomID)
        {
            string query = "SELECT USERID, MESSAGEBODY, SENDDATE FROM MESSAGES WHERE CHATROOMID = " + chatroomID;

            return Database.RetrieveQuery(query);
        }

        //public bool UserHasChatroom(int userID, int chatroomID)
        //{
        //    string query = "SELECT * FROM USER_CHATROOM WHERE USERID = " + userID + " AND CHATROOMID = " + chatroomID;

        //    if (Database.RetrieveQuery(query) != null)
        //        return true;

        //    return false;
        //}

        public List<int> GetUserChatrooms(int userID)
        {
            List<int> returned = new List<int>();

            string query = "SELECT CHATROOMID FROM USER_CHATROOM WHERE USERID = " + userID;

            DataTable dt = Database.RetrieveQuery(query);

            foreach (DataRow d in dt.Rows)
            {
                int room = Convert.ToInt32(d[0]);

                returned.Add(room);
            }

            return returned;
        }

        public void CreateRoom(out int roomId)
        {
            string query = "INSERT INTO CHATROOM VALUES (CHATROOM_SEQ.NEXTVAL)";
            Database.ExecuteQuery(query);
            // get id
            roomId = Database.ReturnIdLastInsertID("CHATROOM_SEQ");
        }

        public List<int> ReturnUserChatrooms(int userID)
        {
            List<int> returned = new List<int>();

            string query = "SELECT CHATROOMID FROM USER_CHATROOM WHERE USERID = " + userID;

            DataTable dt = Database.RetrieveQuery(query);

            foreach (DataRow d in dt.Rows)
            {
                returned.Add(Convert.ToInt32(d[0]));
            }

            return returned;
        }

        public void AddUserToChatroom(int userID, int roomID)
        {
            string query = "INSERT INTO USER_CHATROOM VALUES(" + userID + ", " + roomID + ")";

            Database.ExecuteQuery(query);
        }
    }
}
