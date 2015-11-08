//-----------------------------------------------------------------------
// <copyright file="HoofdForm.Designer.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Proftaak_Chat_DB
{
    /// <summary>
    /// The designer class of the <see cref="HoofdForm" /> form.
    /// </summary>
    public partial class HoofdForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// A list box on the <see cref="HoofdForm"/>
        /// </summary>
        private System.Windows.Forms.ListBox lbContacts;

        /// <summary>
        /// A label on the <see cref="HoofdForm"/>
        /// </summary>
        private System.Windows.Forms.Label label1;

        /// <summary>
        /// A button on the <see cref="HoofdForm"/>
        /// </summary>
        private System.Windows.Forms.Button btnStartChat;

        /// <summary>
        /// A timer on the <see cref="HoofdForm"/>
        /// </summary>
        private System.Windows.Forms.Timer chatCheckTimer;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbContacts = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartChat = new System.Windows.Forms.Button();
            this.chatCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbContacts
            // 
            this.lbContacts.FormattingEnabled = true;
            this.lbContacts.Location = new System.Drawing.Point(12, 25);
            this.lbContacts.Name = "lbContacts";
            this.lbContacts.Size = new System.Drawing.Size(207, 290);
            this.lbContacts.TabIndex = 0;
            this.lbContacts.SelectedIndexChanged += new System.EventHandler(this.lbContacts_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contacts";
            // 
            // btnStartChat
            // 
            this.btnStartChat.Location = new System.Drawing.Point(225, 25);
            this.btnStartChat.Name = "btnStartChat";
            this.btnStartChat.Size = new System.Drawing.Size(106, 23);
            this.btnStartChat.TabIndex = 2;
            this.btnStartChat.Text = "Start Chat";
            this.btnStartChat.UseVisualStyleBackColor = true;
            this.btnStartChat.Click += new System.EventHandler(this.btnStartChat_Click);
            // 
            // chatCheckTimer
            // 
            this.chatCheckTimer.Enabled = true;
            this.chatCheckTimer.Interval = 2000;
            this.chatCheckTimer.Tick += new System.EventHandler(this.chatCheckTimer_Tick);
            // 
            // HoofdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 327);
            this.Controls.Add(this.btnStartChat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbContacts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HoofdForm";
            this.Text = "HoofdForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HoofdForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}