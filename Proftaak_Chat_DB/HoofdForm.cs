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
            Object selectedObj = this.lbContacts.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 && !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)))
                {
                    Form form = new Chat(this, selectedObj);
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
                if (Convert.ToInt32(objType.GetProperty("IsOnline").GetValue(selectedObj, null)) == 1 && !Convert.ToBoolean(objType.GetProperty("IsChatting").GetValue(selectedObj, null)))
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
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            Brush myBrush = new SolidBrush(e.ForeColor);

            // Set custom color based on IsOnline
            Object obj = lbContacts.Items[e.Index];
            if (Convert.ToInt32(obj.GetType().GetProperty("IsOnline").GetValue(obj, null)) == 1) {
                myBrush = new SolidBrush(Color.Green);
            }
            else
            {
                myBrush = new SolidBrush(Color.Red);
            }
            if (Convert.ToBoolean(obj.GetType().GetProperty("IsChatting").GetValue(obj, null)))
            {
                myBrush = new SolidBrush(Color.Blue);
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

        private void HoofdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.admin.SetOffline(gebrID);
            this.p.Close();
        }
    }
}
