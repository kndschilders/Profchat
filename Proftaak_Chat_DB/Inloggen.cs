//-----------------------------------------------------------------------
// <copyright file="Inloggen.cs" company="ICT4Participation">
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
    /// The file containing the code of the <see cref="Inloggen" /> form.
    /// </summary>
    public partial class Inloggen : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Inloggen"/> class.
        /// </summary>
        public Inloggen()
        {
            this.InitializeComponent();

            this.Admin = new Administrator();

            //// databind vrijwilligers
            this.cbUsers.DataSource = this.Admin.Vrijwilligers;
        }

        /// <summary>
        /// Gets or sets a reference to the admin class
        /// </summary>
        private Administrator Admin { get; set; }

        /// <summary>
        /// Log in as the currently selected user
        /// </summary>
        /// <param name="sender">The sender parameter</param>
        /// <param name="e">The EventArgs parameter</param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int gebrID = 0;

            object selectedObj = this.cbUsers.SelectedItem;

            if (selectedObj != null)
            {
                Type objType = selectedObj.GetType();
                gebrID = Convert.ToInt32(objType.GetProperty("ID").GetValue(selectedObj, null));
            }

            this.Admin.SetOnline(gebrID);
            this.Admin.UpdateGebruikers();

            HoofdForm form = new HoofdForm(gebrID);
            form.Show();
            this.Hide();
        }
    }
}