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

            // set label
            if (gebr != null) this.lblPersoon2.Text = gebr.ToString();

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
            List<string> messages = this.admin.ReturnMessages(RoomID);

            foreach (string message in messages)
            {
                this.tbBerichten.Text = message + "\n";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.SendMessage();
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            // delete from
        }
    }
}