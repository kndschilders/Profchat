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

namespace Proftaak_Chat_DB
{
    public partial class Chat : Form
    {
        private Form p;
        private Object gebr;
        private int gebrID;
        private int RoomID;
        private Administrator admin;
        private List<Object> otherOnlinePeople;

        public Chat(Form previous, Object gebruiker, int roomID)
        {
            InitializeComponent();

            this.p = previous;
            this.gebr = gebruiker;
            this.RoomID = roomID;

            this.admin = new Administrator();

            // set gebrID
            if (gebr != null)
            {
                Type objType = gebr.GetType();
                gebrID = Convert.ToInt32(objType.GetProperty("ID").GetValue(gebr, null));
            }

            // get messages
            this.UpdateMessages();

            // focus on messagebox
            this.tbMessage.Focus();
        }

        private void Chat_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.p.Show();
            this.p.Invalidate();
            if (gebr != null)
            {
                Type objType = gebr.GetType();
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(gebr, null)) == 1)
                {
                    // Set IsChatting to true
                    objType.GetProperty("IsChatting").SetValue(gebr, false, null);
                }
            }
        }

        private void SendMessage()
        {
            string message = this.tbMessage.Text.Trim();

            if (message != null)
                this.admin.SendMessage(message, gebrID, RoomID);

            // update messages
            this.UpdateMessages();

            // clear tbMessage
            this.tbMessage.Text = string.Empty;
            this.tbMessage.Focus();
        }

        private void UpdateMessages()
        {
            // Get other users
            List<int> otherUsers = this.admin.GetUsersFromChatroom(this.RoomID);

            List<string> otherUserNames = new List<string>();
            
            foreach(int uID in otherUsers) {
                Object g = this.admin.HaalGebruikerOp(uID);
                Type objType = g.GetType();
                otherUserNames.Add((string)objType.GetProperty("Naam").GetValue(g, null));
            }

            // clear label
            string labelText = string.Empty;

            // set label
            for (int i = 0; i < otherUserNames.Count; i++)
            {
                if (i < otherUserNames.Count - 1)
                    labelText += otherUserNames[i] + ", ";
                else
                    labelText += otherUserNames[i];
            }

            this.lblPersoon2.Text = labelText;

            List<string> messages = this.admin.ReturnMessages(RoomID);

            this.tbBerichten.Text = string.Empty;
            
            foreach (string message in messages)
            {
                this.tbBerichten.Text += message + Environment.NewLine;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.SendMessage();
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close room
            this.admin.DeleteUserFromRoom(gebrID, RoomID);
        }

        private void timerCheckMessages_Tick(object sender, EventArgs e)
        {
            this.UpdateMessages();
            this.UpdateOnlineUsers();
        }

        private void UpdateOnlineUsers()
        {
            this.admin.UpdateGebruikers();

            if (this.otherOnlinePeople == null)
                this.otherOnlinePeople = new List<Object>();


            List<Object> clone = new List<Object>();


            List<Object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<Object>();


            // for each user currently in the chatroom
            List<int> chatroomUsers = this.admin.GetUsersFromChatroom(RoomID);

            foreach (Object obj in newList)
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
            foreach (Object obj in clone)
            {
                Type objType = obj.GetType();
                cloneIDs.Add((int)objType.GetProperty("ID").GetValue(obj));
            }

            bool areEqual = true;

            foreach (Object obj in this.otherOnlinePeople)
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
                    this.cbOnlineUsers.DataSource = null;
                else
                    this.cbOnlineUsers.DataSource = this.otherOnlinePeople;
            }
        }

        private void btnVoegToe_Click(object sender, EventArgs e)
        {
            Object selectedObj = this.cbOnlineUsers.SelectedItem;

            if (selectedObj == null) return;

            Type objType = selectedObj.GetType();

            int uID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));

            this.admin.AddUserToChatroom(uID, this.RoomID);
        }
    }
}