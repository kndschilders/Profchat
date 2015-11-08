//-----------------------------------------------------------------------
// <copyright file="Chat.Designer.cs" company="ICT4Participation">
//     Copyright (c) ICT4Participation. All rights reserved.
// </copyright>
// <author>ICT4Participation</author>
//-----------------------------------------------------------------------
namespace Proftaak_Chat_DB
{
    /// <summary>
    /// The designer class of the <see cref="Chat" /> form.
    /// </summary>
    public partial class Chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// A label on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.Label label1;

        /// <summary>
        /// A label on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.Label lblPersoon2;

        /// <summary>
        /// A text box on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.TextBox tbBerichten;

        /// <summary>
        /// A text box on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.TextBox tbMessage;

        /// <summary>
        /// A button on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.Button btnSend;

        /// <summary>
        /// A timer on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.Timer timerCheckMessages;

        /// <summary>
        /// A combo box on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.ComboBox cbOnlineUsers;

        /// <summary>
        /// A button on the <see cref="Chat"/> form
        /// </summary>
        private System.Windows.Forms.Button btnVoegToe;

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
            this.label1 = new System.Windows.Forms.Label();
            this.lblPersoon2 = new System.Windows.Forms.Label();
            this.tbBerichten = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.timerCheckMessages = new System.Windows.Forms.Timer(this.components);
            this.cbOnlineUsers = new System.Windows.Forms.ComboBox();
            this.btnVoegToe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gebruikers in deze chatroom:";
            // 
            // lblPersoon2
            // 
            this.lblPersoon2.AutoSize = true;
            this.lblPersoon2.Location = new System.Drawing.Point(12, 29);
            this.lblPersoon2.Name = "lblPersoon2";
            this.lblPersoon2.Size = new System.Drawing.Size(35, 13);
            this.lblPersoon2.TabIndex = 1;
            this.lblPersoon2.Text = "label2";
            // 
            // tbBerichten
            // 
            this.tbBerichten.Location = new System.Drawing.Point(12, 54);
            this.tbBerichten.Multiline = true;
            this.tbBerichten.Name = "tbBerichten";
            this.tbBerichten.ReadOnly = true;
            this.tbBerichten.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbBerichten.Size = new System.Drawing.Size(260, 298);
            this.tbBerichten.TabIndex = 2;
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(12, 358);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessage.Size = new System.Drawing.Size(260, 85);
            this.tbMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(12, 449);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(260, 44);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Stuur bericht";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // timerCheckMessages
            // 
            this.timerCheckMessages.Enabled = true;
            this.timerCheckMessages.Interval = 500;
            this.timerCheckMessages.Tick += new System.EventHandler(this.timerCheckMessages_Tick);
            // 
            // cbOnlineUsers
            // 
            this.cbOnlineUsers.FormattingEnabled = true;
            this.cbOnlineUsers.Location = new System.Drawing.Point(160, 6);
            this.cbOnlineUsers.Name = "cbOnlineUsers";
            this.cbOnlineUsers.Size = new System.Drawing.Size(121, 21);
            this.cbOnlineUsers.TabIndex = 4;
            // 
            // btnVoegToe
            // 
            this.btnVoegToe.Location = new System.Drawing.Point(206, 29);
            this.btnVoegToe.Name = "btnVoegToe";
            this.btnVoegToe.Size = new System.Drawing.Size(75, 23);
            this.btnVoegToe.TabIndex = 5;
            this.btnVoegToe.Text = "Voeg Toe";
            this.btnVoegToe.UseVisualStyleBackColor = true;
            this.btnVoegToe.Click += new System.EventHandler(this.btnVoegToe_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 505);
            this.Controls.Add(this.btnVoegToe);
            this.Controls.Add(this.cbOnlineUsers);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.tbBerichten);
            this.Controls.Add(this.lblPersoon2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Chat";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chat_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chat_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}