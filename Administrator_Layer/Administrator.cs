//-----------------------------------------------------------------------
// <copyright file="Administrator.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Administrator_Layer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Class_Layer;

    /// <summary>
    /// This class is used to let the GUIs communicate with the class layer
    /// </summary>
    public class Administrator
    {
        /// <summary>
        /// A reference to the data class
        /// </summary>
        private Data data = new Data();

        /// <summary>
        /// Initializes a new instance of the <see cref="Administrator"/> class.
        /// </summary>
        public Administrator()
        {
            this.Vrijwilligers = new List<object>();

            this.UpdateGebruikers();
        }

        /// <summary>
        /// Gets a list of user objects
        /// </summary>
        public List<object> Vrijwilligers { get; private set; }

        /// <summary>
        /// Update the list of user objects
        /// </summary>
        public void UpdateGebruikers()
        {
            List<Gebruiker> list = this.data.HaalGebruikersOp();

            this.Vrijwilligers.Clear();

            if (list != null)
            {
                foreach (Gebruiker g in list)
                {
                    this.Vrijwilligers.Add(g);
                }
            }
        }

        /// <summary>
        /// Set the IsOnline property of the user to true
        /// </summary>
        /// <param name="gebrID">The ID of the user</param>
        /// <returns>Returns whether or not the operation has succeeded</returns>
        public bool SetOnline(int gebrID)
        {
            return this.data.UpdateOnlineStatus(true, gebrID);
        }

        /// <summary>
        /// Set the IsOnline property of the user to false
        /// </summary>
        /// <param name="gebrID">The ID of the user</param>
        /// <returns>Returns whether or not the operation has succeeded</returns>
        public bool SetOffline(int gebrID)
        {
            return this.data.UpdateOnlineStatus(false, gebrID);
        }

        /// <summary>
        /// Send a new message
        /// </summary>
        /// <param name="message">The actual message</param>
        /// <param name="gebrID">The ID of the owner of the message</param>
        /// <param name="roomID">The room number the message has to be send to</param>
        public void SendMessage(string message, int gebrID, int roomID)
        {
            this.data.SendMessage(message, gebrID, roomID);
        }

        /// <summary>
        /// Get a list of messages of the given room
        /// </summary>
        /// <param name="chatroomID">The room number where to retrieve the messages from</param>
        /// <returns>Returns a list of messages</returns>
        public List<string> ReturnMessages(int chatroomID)
        {
            List<string> returned = new List<string>();

            DataTable dt = this.data.ReturnMessages(chatroomID);

            foreach (DataRow row in dt.Rows)
            {
                string username;
                string message;
                string senddate;

                username = Convert.ToString(row.ItemArray[0]);
                senddate = Convert.ToString(row.ItemArray[1]);
                message = Convert.ToString(row.ItemArray[2]);

                returned.Add(string.Format("{0}<{1}>: {2}", username, senddate, message));
            }

            return returned;
        }

        /// <summary>
        /// Get the object of a user given the ID
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <returns>Returns a user object who belongs to the given ID</returns>
        public object HaalGebruikerOp(int userID)
        {
            return this.data.HaalGebruikerOp(userID);
        }

        /// <summary>
        /// Get a list of chat rooms the given user is currently connected to
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <returns>Returns a list of room numbers</returns>
        public List<int> GetUserChatrooms(int userID)
        {
            return this.data.GetUserChatrooms(userID);
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="roomID">Return the roomID of the inserted room</param>
        public void CreateRoom(out int roomID)
        {
            this.data.CreateRoom(out roomID);
        }

        /// <summary>
        /// Add a user to the given chat room
        /// </summary>
        /// <param name="userID">The ID of the new user</param>
        /// <param name="roomID">The ID of the chat room</param>
        public void AddUserToChatroom(int userID, int roomID)
        {
            this.data.AddUserToChatroom(userID, roomID);
        }

        /// <summary>
        /// Get all users currently connected to the chat room
        /// </summary>
        /// <param name="roomID">The ID of the chat room</param>
        /// <returns>Returns a list of user IDs</returns>
        public List<int> GetUsersFromChatroom(int roomID)
        {
            List<int> returned = new List<int>();

            DataTable dt = this.data.GetUsersFromChatroom(roomID);

            foreach (DataRow d in dt.Rows)
            {
                returned.Add(Convert.ToInt32(d[0]));
            }

            return returned;
        }

        /// <summary>
        /// Delete a user from the current chat room
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <param name="roomID">The ID of the chat room</param>
        public void DeleteUserFromRoom(int userID, int roomID)
        {
            this.data.DeleteUserFromRoom(userID, roomID);
        }
    }
}
