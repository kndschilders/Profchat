//-----------------------------------------------------------------------
// <copyright file="Chatroom.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Class_Layer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Database_Layer;

    /// <summary>
    /// Keeps track of the information about a chat room
    /// </summary>
    public class Chatroom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chatroom"/> class.
        /// </summary>
        public Chatroom()
        {
            this.Users = new List<Gebruiker>();

            Database.ExecuteQuery("SELECT CHATROOMID FROM USER_CHATROOM WHERE USERID = 10001");
        }

        /// <summary>
        /// Gets the ID of the chat room
        /// </summary>
        public int ChatroomID { get; private set; }

        /// <summary>
        /// Gets or sets a list of users in this chat room
        /// </summary>
        public List<Gebruiker> Users { get; set; }

        /// <summary>
        /// Send a message to the database
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <param name="ownerID">The ID of the owner of the message</param>
        /// <returns>Returns whether or not the operation has succeeded</returns>
        public bool SendMessage(string message, int ownerID)
        {
            string query = "INSERT INTO MESSAGES VALUE (MESSAGES_SEQ.NEXTVAL, " + ownerID + ", " + this.ChatroomID + ", " + message + ", CURRENT_TIMESTAMP)";
            Database.ExecuteQuery(query);
            return false;
        }
    }
}
