//-----------------------------------------------------------------------
// <copyright file="HoofdForm.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Proftaak_Chat_DB
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Administrator_Layer;

    /// <summary>
    /// The file containing the code of the <see cref="HoofdForm" /> form.
    /// </summary>
    public partial class HoofdForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HoofdForm"/> class
        /// </summary>
        /// <param name="previous">The previous form</param>
        /// <param name="gebruikerID">The ID of the user</param>
        public HoofdForm(Form previous, int gebruikerID)
        {
            this.InitializeComponent();

            this.p = previous;
            this.gebrID = gebruikerID;

            this.admin = new Administrator();
            this.lbContacts.DrawMode = DrawMode.OwnerDrawFixed;
            this.lbContacts.DrawItem += new DrawItemEventHandler(this.listBox_DrawItem);

            //// Fill lbContacts
            this.lbContacts.Items.Clear();
            List<object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<object>();

            foreach (object obj in newList)
            {
                Type objType = obj.GetType();
                if (objType.GetProperty("IsOnline") != null)
                {
                    if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1)
                    {
                        this.lbContacts.ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.lbContacts.ForeColor = Color.Red;
                    }
                }

                this.lbContacts.Items.Add(obj);
            }

            //// Set selectedindex of lbContacts
            this.lbContacts.SelectedIndex = 0;

            //// get gebruiker
            this.gebruiker = this.admin.HaalGebruikerOp(this.gebrID);
        }

        /// <summary>
        /// Gets or sets a reference of the <see cref="Administration"/> class
        /// </summary>
        private Administrator admin { get; set; }

        /// <summary>
        /// Gets or sets a reference of the previous form
        /// </summary>
        private Form p { get; set; }

        /// <summary>
        /// Gets or sets a reference of the user ID
        /// </summary>
        private int gebrID { get; set; }

        /// <summary>
        /// Gets or sets a reference of the user object
        /// </summary>
        private object gebruiker { get; set; }

        /// <summary>
        /// Start a new chat with the selected person on button click
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void btnStartChat_Click(object sender, EventArgs e)
        {
            object selectedObj = this.lbContacts.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();

                //// Get id of selected person
                int client2ID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));

                int roomID;

                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 && !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)))
                {
                    this.admin.CreateRoom(out roomID);

                    //// Set IsChatting to true
                    objType.GetProperty("IsChatting").SetValue(selectedObj, true, null);
                    this.lbContacts.Invalidate();

                    //// Add both users to user_chatroom
                    this.admin.AddUserToChatroom(this.gebrID, roomID);
                    this.admin.AddUserToChatroom(client2ID, roomID);
                }
                else
                {
                    this.btnStartChat.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Update the start chat button on list box selected index change
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void lbContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedObj = this.lbContacts.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 &&
                    !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)) &&
                    Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null)) != this.gebrID)
                {
                    this.btnStartChat.Enabled = true;
                }
                else
                {
                    this.btnStartChat.Enabled = false;
                }
            }
        }

        /// <summary>
        /// A custom draw item method to allow for different colors per item
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //// Draw the background of the ListBox control for each item.
                e.DrawBackground();

                Brush myBrush = new SolidBrush(e.ForeColor);

                //// Set custom color based on IsOnline
                object obj = this.lbContacts.Items[e.Index];
                if (Convert.ToInt32(obj.GetType().GetProperty("IsOnline").GetValue(obj, null)) == 1)
                {
                    //// online user
                    myBrush = new SolidBrush(Color.Green);
                }
                else
                {
                    //// offline user
                    myBrush = new SolidBrush(Color.Red);
                }

                if (Convert.ToBoolean(obj.GetType().GetProperty("IsChatting").GetValue(obj, null)))
                {
                    //// user currently chatting
                    myBrush = new SolidBrush(Color.Blue);
                }

                if (Convert.ToInt32(obj.GetType().GetProperty("ID").GetValue(obj, null)) == this.gebrID)
                {
                    //// yourself
                    myBrush = new SolidBrush(Color.DarkGray);
                }

                //// Draw the current item text based on the current font and the custom brush settings.
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                //// If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
            }
            catch (Exception)
            {
                //// ignore exception
            }
        }

        /// <summary>
        /// Set the user to offline and close the previous form on form close
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void HoofdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.admin.SetOffline(this.gebrID);
            this.p.Close();
        }

        /// <summary>
        /// Check for online people
        /// </summary>
        /// <param name="selectedindex">The selected index of the list box to prevent it from resetting</param>
        private void CheckOnlinePeople(int selectedindex)
        {
            this.admin.UpdateGebruikers();

            //// Fill lbContacts
            this.lbContacts.Items.Clear();
            List<object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<object>();

            foreach (object obj in newList)
            {
                Type objType = obj.GetType();

                if (objType.GetProperty("IsOnline") != null)
                {
                    if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1)
                    {
                        this.lbContacts.ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.lbContacts.ForeColor = Color.Red;
                    }
                }

                this.lbContacts.Items.Add(obj);
            }

            this.lbContacts.SelectedIndex = selectedindex;
        }

        /// <summary>
        /// Check for new chats
        /// </summary>
        private void CheckForNewChats()
        {
            if (this.gebruiker != null)
            {
                List<int> rooms = this.admin.GetUserChatrooms(this.gebrID);

                //// if user is currently in rooms
                if (rooms != null)
                {
                    //// check if gebruiker.currentrooms contains a room in rooms
                    Type objType = this.gebruiker.GetType();
                    ReadOnlyCollection<int> roomsList = (ReadOnlyCollection<int>)objType.GetMethod("GetCurrentRooms").Invoke(this.gebruiker, null);
                    
                    foreach (int room in rooms)
                    {
                        if (!roomsList.Contains(room))
                        {
                            objType.GetMethod("AddRoom").Invoke(this.gebruiker, new object[] { room });
                            Form form = new Chat(this, this.gebruiker, room);
                            form.Show();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check for new chats and online people on timer tick
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void chatCheckTimer_Tick(object sender, EventArgs e)
        {
            this.CheckForNewChats();
            this.CheckOnlinePeople(this.lbContacts.SelectedIndex);
        }
    }
}
