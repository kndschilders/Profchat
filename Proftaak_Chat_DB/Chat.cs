using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proftaak_Chat_DB
{
    public partial class Chat : Form
    {
        private Form p;
        private Object gebr;
        public Chat(Form previous, Object gebruiker)
        {
            InitializeComponent();

            this.p = previous;
            this.gebr = gebruiker;

            // set label
            if (gebr != null) this.lblPersoon2.Text = gebr.ToString();
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
    }
}