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
    public partial class Inloggen : Form
    {
        private Administrator admin;
        public Inloggen()
        {
            InitializeComponent();

            this.admin = new Administrator();

            // databind vrijwilligers
            this.cbUsers.DataSource = this.admin.Vrijwilligers;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int gebrID = 0;

            Object selectedObj = this.cbUsers.SelectedItem;
            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();
                gebrID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));
            }

            this.admin.SetOnline(gebrID);
            this.admin.UpdateGebruikers();

            HoofdForm form = new HoofdForm(this, gebrID);
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HoofdForm form = new HoofdForm(this);
            form.Show();
            this.Hide();
        }
    }
}