//-----------------------------------------------------------------------
// <copyright file="Chat.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Proftaak_Chat_DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Administrator_Layer;

    /// <summary>
    /// The file containing the code of the <see cref="Chat" /> form.
    /// </summary>
    public partial class Chat : Form
    {
        /// <summary>
        /// A reference to the last form
        /// </summary>
        private Form p;

        /// <summary>
        /// A reference of the user
        /// </summary>
        private object gebr;

        /// <summary>
        /// A reference of the user ID
        /// </summary>
        private int gebrID;

        /// <summary>
        /// A reference of the room ID
        /// </summary>
        private int roomID;

        /// <summary>
        /// A reference of the <see cref="Administrator"/> class
        /// </summary>
        private Administrator admin;

        /// <summary>
        /// A list of all online users not currently connected to this room
        /// </summary>
        private List<object> otherOnlinePeople;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat"/> class
        /// </summary>
        /// <param name="previous">The previous form</param>
        /// <param name="gebruiker">The user</param>
        /// <param name="roomID">The chat room ID</param>
        public Chat(Form previous, object gebruiker, int roomID)
        {
            this.InitializeComponent();

            this.p = previous;
            this.gebr = gebruiker;
            this.roomID = roomID;

            this.admin = new Administrator();

            // set gebrID
            if (this.gebr != null)
            {
                Type objType = this.gebr.GetType();
                this.gebrID = Convert.ToInt32(objType.GetProperty("ID").GetValue(this.gebr, null));
            }

            // get messages
            this.UpdateMessages();

            // focus on messagebox
            this.tbMessage.Focus();
        }

        /// <summary>
        /// Set the IsChatting value to false when closing a chat room
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The FormClosedEventArgs</param>
        private void Chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.p.Show();
            this.p.Invalidate();
            if (this.gebr != null)
            {
                Type objType = this.gebr.GetType();
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(this.gebr, null)) == 1)
                {
                    // Set IsChatting to true
                    objType.GetProperty("IsChatting").SetValue(this.gebr, false, null);
                }
            }
        }

        /// <summary>
        /// Send a message
        /// </summary>
        private void SendMessage()
        {
            string message = this.tbMessage.Text.Trim();

            if (message != null)
            {
                this.admin.SendMessage(message, this.gebrID, this.roomID);
            }

            // update messages
            this.UpdateMessages();

            // clear tbMessage
            this.tbMessage.Text = string.Empty;
            this.tbMessage.Focus();
        }

        /// <summary>
        /// Update the messages of the chat room
        /// </summary>
        private void UpdateMessages()
        {
            // Get other users
            List<int> otherUsers = this.admin.GetUsersFromChatroom(this.roomID);

            List<string> otherUserNames = new List<string>();
            
            foreach (int uID in otherUsers)
            {
                object g = this.admin.HaalGebruikerOp(uID);
                Type objType = g.GetType();
                otherUserNames.Add((string)objType.GetProperty("Naam").GetValue(g, null));
            }

            // clear label
            string labelText = string.Empty;

            // set label
            for (int i = 0; i < otherUserNames.Count; i++)
            {
                if (i < otherUserNames.Count - 1)
                {
                    labelText += otherUserNames[i] + ", ";
                }
                else
                {
                    labelText += otherUserNames[i];
                }
            }

            this.lblPersoon2.Text = labelText;

            List<string> messages = this.admin.ReturnMessages(this.roomID);

            this.tbBerichten.Text = string.Empty;
            
            foreach (string message in messages)
            {
                this.tbBerichten.Text += message + Environment.NewLine;
            }
        }

        /// <summary>
        /// Send a message on click
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The EventArgs parameter</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            this.SendMessage();
        }

        /// <summary>
        /// Delete the user from the chat room on close
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The EventArgs parameter</param>
        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.admin.DeleteUserFromRoom(this.gebrID, this.roomID);
        }

        /// <summary>
        /// Call update methods on timer tick
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The EventArgs parameter</param>
        private void timerCheckMessages_Tick(object sender, EventArgs e)
        {
            this.UpdateMessages();
            this.UpdateOnlineUsers();
        }

        /// <summary>
        /// Update the currently online users
        /// </summary>
        private void UpdateOnlineUsers()
        {
            this.admin.UpdateGebruikers();

            if (this.otherOnlinePeople == null)
            {
                this.otherOnlinePeople = new List<object>();
            }

            List<object> clone = new List<object>();
            List<object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<object>();
            List<int> chatroomUsers = this.admin.GetUsersFromChatroom(this.roomID);

            foreach (object obj in newList)
            {
                Type objType = obj.GetType();
                if (objType.GetProperty("IsOnline") != null &&
                    Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1 &&
                    !chatroomUsers.Contains((int)objType.GetProperty("ID").GetValue(obj, null)))
                {
                    clone.Add(obj);
                }
            }

            List<int> cloneIDs = new List<int>();
            List<int> otherOnlinePeopleIDs = new List<int>();

            // fill cloneID list
            foreach (object obj in clone)
            {
                Type objType = obj.GetType();
                cloneIDs.Add((int)objType.GetProperty("ID").GetValue(obj));
            }

            bool areEqual = true;

            foreach (object obj in this.otherOnlinePeople)
            {
                Type objType = obj.GetType();
                int objID = (int)objType.GetProperty("ID").GetValue(obj);
                otherOnlinePeopleIDs.Add(objID);

                if (!cloneIDs.Contains(objID))
                {
                    areEqual = false;
                    break;
                }
            }

            foreach (int cloneID in cloneIDs)
            {
                if (!otherOnlinePeopleIDs.Contains(cloneID))
                {
                    areEqual = false;
                    break;
                }
            }

            if (!areEqual)
            {
                this.otherOnlinePeople = clone;

                if (this.otherOnlinePeople.Count == 0)
                {
                    this.cbOnlineUsers.DataSource = null;
                }
                else
                {
                    this.cbOnlineUsers.DataSource = this.otherOnlinePeople;
                }
            }
        }

        /// <summary>
        /// Add a user to the chat room
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The EventArgs parameter</param>
        private void btnVoegToe_Click(object sender, EventArgs e)
        {
            object selectedObj = this.cbOnlineUsers.SelectedItem;

            if (selectedObj == null)
            {
                return;
            }

            Type objType = selectedObj.GetType();

            int uID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));

            this.admin.AddUserToChatroom(uID, this.roomID);
        }
    }
}