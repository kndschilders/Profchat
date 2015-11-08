//-----------------------------------------------------------------------
// <copyright file="Data.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Class_Layer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Database_Layer;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// A subclass to communicate from the administration layer to the database
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>Returns a list of all users</returns>
        public List<Gebruiker> HaalGebruikersOp()
        {
            List<Gebruiker> returned = new List<Gebruiker>();

            //// haal alle ids op van hoofd database
            List<Gebruiker> hoofddbGebruikers = new List<Gebruiker>();
            string query = "SELECT \"ID\", \"Name\" FROM \"Acc\"";
            DataTable dt = Database.HaalGebruikersOp(query);

            foreach (DataRow d in dt.Rows)
            {
                int id = Convert.ToInt32(d[0]);
                string name = Convert.ToString(d[1]);

                hoofddbGebruikers.Add(new Gebruiker(id, name, 0));
            }

            //// haal alle ids op van chat database
            List<Gebruiker> chatdbGebruikers = new List<Gebruiker>();
            query = "SELECT ID, NAME, ISONLINE FROM \"USER\"";
            dt = Database.RetrieveQuery(query);

            foreach (DataRow d in dt.Rows)
            {
                int id = Convert.ToInt32(d[0]);
                string name = Convert.ToString(d[1]);
                int isOnline = Convert.ToInt32(d[2]);

                chatdbGebruikers.Add(new Gebruiker(id, name, isOnline));
            }

            string query2 = string.Empty;

            //// foreach hoofdformdbID in hoofdformgebruikers
            foreach (Gebruiker g in hoofddbGebruikers)
            {
                if (chatdbGebruikers.Contains(g))
                {
                    //// als hij bestaat -> update row met het id
                    query2 = string.Format("UPDATE \"USER\" SET NAAM = '{0}', ISONLINE = {1} WHERE ID = {2}", g.Naam, g.IsOnline, g.ID);
                }
                else
                {
                    //// als hij niet bestaat -> insert row
                    query2 = string.Format("INSERT INTO \"USER\"(ID, NAAM, ISONLINE) VALUES ({0}, '{1}', {2})", g.ID, g.Naam, g.IsOnline);
                }

                //// execute query2
                Database.ExecuteQuery(query2);
            }

            //// select all users from chat database
            string query3 = "SELECT ID, NAAM, ISONLINE FROM \"USER\"";
            dt = Database.RetrieveQuery(query3);

            foreach (DataRow d in dt.Rows)
            {
                int id = Convert.ToInt32(d[0]);
                string naam = Convert.ToString(d[1]);
                int isonline = Convert.ToInt32(d[2]);

                returned.Add(new Gebruiker(id, naam, isonline));
            }

            return returned;
        }

        /// <summary>
        /// Get a user object given the user ID
        /// </summary>
        /// <param name="gebruikerID">The ID of the user</param>
        /// <returns>Returns the user object</returns>
        public Gebruiker HaalGebruikerOp(int gebruikerID)
        {
            string query = "SELECT ID, NAAM, ISONLINE FROM \"USER\" WHERE ID = " + gebruikerID;

            DataTable dt = Database.RetrieveQuery(query);

            if (dt.Rows.Count != 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["ID"]);
                string naam = Convert.ToString(dt.Rows[0]["NAAM"]);
                int isOnline = Convert.ToInt32(dt.Rows[0]["ISONLINE"]);

                return new Gebruiker(id, naam, isOnline);
            }

            return null;
        }

        /// <summary>
        /// Update the online status of a user
        /// </summary>
        /// <param name="newStatus">The new online status of the user</param>
        /// <param name="gebrID">The user of whom to change the online status</param>
        /// <returns>Returns whether or not the operation has succeeded</returns>
        public bool UpdateOnlineStatus(bool newStatus, int gebrID)
        {
            string query = string.Empty;

            if (newStatus)
            {
                query = "UPDATE \"USER\" SET ISONLINE = 1 WHERE ID = " + gebrID;
            }
            else
            {
                query = "UPDATE \"USER\" SET ISONLINE = 0 WHERE ID = " + gebrID;
            }
            
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

        /// <summary>
        /// Insert a message in the database
        /// </summary>
        /// <param name="message">The message to insert</param>
        /// <param name="gebrID">The ID of the owner of the message</param>
        /// <param name="chatroomID">The chat room the message has been sent from</param>
        public void SendMessage(string message, int gebrID, int chatroomID)
        {
            string query = "INSERT INTO MESSAGES VALUES(MESSAGES_SEQ.NEXTVAL, " + gebrID + ", " + chatroomID + ", " + "'" + message + "'" + ", CURRENT_TIMESTAMP)";

            Console.WriteLine(query);

            Database.ExecuteQuery(query);
        }

        /// <summary>
        /// Get all messages of a chat room
        /// </summary>
        /// <param name="chatroomID">The ID of the chat room</param>
        /// <returns>Returns a DataTable containing all messages of the chat room</returns>
        public DataTable ReturnMessages(int chatroomID)
        {
            string query = "SELECT U.NAAM, TO_CHAR(M.SENDDATE, 'hh24:mi:ss') AS SENDDATE, M.MESSAGEBODY FROM \"USER\" U, MESSAGES M WHERE M.USERID = U.ID AND CHATROOMID = " + chatroomID + " ORDER BY M.SENDDATE";

            return Database.RetrieveQuery(query);
        }

        /// <summary>
        /// Get all chat rooms the given user is currently connected to
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <returns>Returns a list of chat room numbers</returns>
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

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="roomId">The ID of the new chat room</param>
        public void CreateRoom(out int roomId)
        {
            string query = "INSERT INTO CHATROOM VALUES (CHATROOM_SEQ.NEXTVAL)";
            Database.ExecuteQuery(query);

            roomId = Database.ReturnIdLastInsertID("CHATROOM_SEQ");
        }

        /// <summary>
        /// Add a user to the given room
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <param name="roomID">The ID of the chat room</param>
        public void AddUserToChatroom(int userID, int roomID)
        {
            string query = "INSERT INTO USER_CHATROOM VALUES(" + userID + ", " + roomID + ")";

            Database.ExecuteQuery(query);
        }

        /// <summary>
        /// Get all users from a chat room
        /// </summary>
        /// <param name="roomID">The ID of the chat room</param>
        /// <returns>Returns a DataTable containing all the user IDs</returns>
        public DataTable GetUsersFromChatroom(int roomID)
        {
            string query = "SELECT USERID FROM USER_CHATROOM WHERE CHATROOMID = " + roomID;

            return Database.RetrieveQuery(query);
        }

        /// <summary>
        /// Delete a user from a chat room
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <param name="roomID">The ID of the chat room</param>
        public void DeleteUserFromRoom(int userID, int roomID)
        {
            string query = "DELETE FROM USER_CHATROOM WHERE USERID = " + userID + " AND CHATROOMID = " + roomID;

            Database.ExecuteQuery(query);
        }
    }
}
