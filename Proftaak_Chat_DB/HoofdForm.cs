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
using System.Reflection;

namespace Proftaak_Chat_DB
{
    public partial class HoofdForm : Form
    {
        Administrator admin;
        Form p;
        int gebrID;
        
        public HoofdForm(Form previous)
        {
            InitializeComponent();

            this.p = previous;

            admin = new Administrator();
            lbContacts.DrawMode = DrawMode.OwnerDrawFixed;
            lbContacts.DrawItem += new DrawItemEventHandler(listBox_DrawItem);

            // Fill lbContacts
            this.lbContacts.Items.Clear();
            List<Object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<Object>();
            foreach(Object obj in newList)//this.admin.Vrijwilligers)
            {
                Type objType = obj.GetType();
                if (objType.GetProperty("IsOnline") != null)
                    if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1)
                        this.lbContacts.ForeColor = Color.Blue;
                    else
                        this.lbContacts.ForeColor = Color.Red;
                    this.lbContacts.Items.Add(obj);
            }

            // Set selectedindex of lbContacts
            this.lbContacts.SelectedIndex = 0;
        }

        public HoofdForm(Form previous, int gebruikerID)
        {
            InitializeComponent();

            this.p = previous;
            this.gebrID = gebruikerID;

            admin = new Administrator();
            lbContacts.DrawMode = DrawMode.OwnerDrawFixed;
            lbContacts.DrawItem += new DrawItemEventHandler(listBox_DrawItem);

            // Fill lbContacts
            this.lbContacts.Items.Clear();
            List<Object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<Object>();
            foreach (Object obj in newList)//this.admin.Vrijwilligers)
            {
                Type objType = obj.GetType();
                if (objType.GetProperty("IsOnline") != null)
                    if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1)
                        this.lbContacts.ForeColor = Color.Blue;
                    else
                        this.lbContacts.ForeColor = Color.Red;
                this.lbContacts.Items.Add(obj);
            }

            // Set selectedindex of lbContacts
            this.lbContacts.SelectedIndex = 0;
        }

        private void btnStartChat_Click(object sender, EventArgs e)
        {
            // check if player is in user_chatroom
            //DataTable chatRooms = this.admin.GetUserChatrooms(gebrID);
            //if (chatRooms != null)
            //{

            //}




            Object selectedObj = this.lbContacts.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();


                // get id of selected person
                int client2ID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));


                // create new chatroom with person, store chatroomID
                int roomID;
                this.admin.CreateRoom(out roomID);


                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 && !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)))
                {
                    Form form = new Chat(this, selectedObj, roomID);
                    form.Show();
                    // Set IsChatting to true
                    objType.GetProperty("IsChatting").SetValue(selectedObj, true, null);
                    this.lbContacts.Invalidate();
                }
                else
                {
                    this.btnStartChat.Enabled = false;
                }
            }
        }

        private void lbContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedObj = this.lbContacts.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 &&
                    !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)) &&
                    Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null)) != gebrID)
                    this.btnStartChat.Enabled = true;
                else
                    this.btnStartChat.Enabled = false;
            }
        }

        private void lbContacts_DoubleClick(object sender, EventArgs e)
        {
            Object selectedObj = this.lbContacts.SelectedItem;
            Type objType = selectedObj.GetType();
            PropertyInfo[] array = objType.GetProperties();

            foreach (PropertyInfo p in array)
            {
                if (p != null)
                {
                    MessageBox.Show(Convert.ToString(p.GetValue(selectedObj, null)));
                }
            }
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Draw the background of the ListBox control for each item.
                e.DrawBackground();

                Brush myBrush = new SolidBrush(e.ForeColor);

                // Set custom color based on IsOnline
                Object obj = lbContacts.Items[e.Index];
                if (Convert.ToInt32(obj.GetType().GetProperty("IsOnline").GetValue(obj, null)) == 1)
                {
                    // online user
                    myBrush = new SolidBrush(Color.Green);
                }
                else
                {
                    // offline user
                    myBrush = new SolidBrush(Color.Red);
                }
                if (Convert.ToBoolean(obj.GetType().GetProperty("IsChatting").GetValue(obj, null)))
                {
                    // user currently chatting
                    myBrush = new SolidBrush(Color.Blue);
                }
                if (Convert.ToInt32(obj.GetType().GetProperty("ID").GetValue(obj, null)) == gebrID)
                {
                    // yourself
                    myBrush = new SolidBrush(Color.DarkGray);
                }

                // Draw the current item text based on the current 
                // Font and the custom brush settings.
                //
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                            e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle 
                // around the selected item.
                //
                e.DrawFocusRectangle();
            }
            catch(Exception)
            {
                // ignore exception
            }
        }

        private void HoofdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.admin.SetOffline(gebrID);
            this.p.Close();
        }

        private void CheckOnlinePeople(int selectedindex)
        {
            this.admin.UpdateGebruikers();

            // Fill lbContacts
            this.lbContacts.Items.Clear();
            List<Object> newList = this.admin.Vrijwilligers.OrderBy(i => i.GetType().GetProperty("IsOnline").GetValue(i, null)).Reverse().ToList<Object>();
            foreach (Object obj in newList)//this.admin.Vrijwilligers)
            {
                Type objType = obj.GetType();
                if (objType.GetProperty("IsOnline") != null)
                    if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(obj, null)) == 1)
                        this.lbContacts.ForeColor = Color.Blue;
                    else
                        this.lbContacts.ForeColor = Color.Red;
                this.lbContacts.Items.Add(obj);
            }
            this.lbContacts.SelectedIndex = selectedindex;
        }

        private void CheckForNewChats()
        {
            List<int> rooms = this.admin.GetUserChatrooms(gebrID);

            // if user is currently in rooms
            if (rooms != null)
            {
                foreach (int room in rooms)
                {
                    Form form = new Chat(this, lbContacts.SelectedItem, room);
                    form.Show();
                }
            }
        }

        private void chatCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckForNewChats();
            CheckOnlinePeople(this.lbContacts.SelectedIndex);
        }
    }
}
