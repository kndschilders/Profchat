//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Proftaak_Chat_DB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// The entry point of the application
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The ID of the user</param>
        [STAThread]
        public static void Main(string[] args)
        {
            string[] arguments = Environment.GetCommandLineArgs();

            int id = 0;

            foreach (string a in arguments)
            {
                int.TryParse(a, out id);
            }

            int[] ids = new int[] { 001, 022, 025};
            Random rnd = new Random();
            id = ids[rnd.Next(ids.Length)];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HoofdForm(id));
        }
    }
}