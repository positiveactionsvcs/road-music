using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RoadMusic.UI.Forms
{
	partial class AboutForm : Form
	{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
            this.btnOK = new System.Windows.Forms.Button();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblID3Sharp = new System.Windows.Forms.Label();
            this.lnkID3Sharp = new System.Windows.Forms.LinkLabel();
            this.picDonate = new System.Windows.Forms.PictureBox();
            this.lblCodestud = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(264, 159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramName.Location = new System.Drawing.Point(21, 19);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(114, 24);
            this.lblProgramName.TabIndex = 1;
            this.lblProgramName.Text = "RoadMusic";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(22, 52);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(61, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version: x.x";
            // 
            // lblID3Sharp
            // 
            this.lblID3Sharp.AutoSize = true;
            this.lblID3Sharp.Location = new System.Drawing.Point(22, 103);
            this.lblID3Sharp.Name = "lblID3Sharp";
            this.lblID3Sharp.Size = new System.Drawing.Size(205, 13);
            this.lblID3Sharp.TabIndex = 5;
            this.lblID3Sharp.Text = "References ID3Sharp by Chris Woodbury ";
            // 
            // lnkID3Sharp
            // 
            this.lnkID3Sharp.AutoSize = true;
            this.lnkID3Sharp.Location = new System.Drawing.Point(22, 125);
            this.lnkID3Sharp.Name = "lnkID3Sharp";
            this.lnkID3Sharp.Size = new System.Drawing.Size(204, 13);
            this.lnkID3Sharp.TabIndex = 6;
            this.lnkID3Sharp.TabStop = true;
            this.lnkID3Sharp.Text = "http://sourceforge.net/projects/id3sharp/";
            this.lnkID3Sharp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkID3Sharp_LinkClicked);
            // 
            // picDonate
            // 
            this.picDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDonate.Image = global::RoadMusic.UI.Properties.Resources.Donate;
            this.picDonate.Location = new System.Drawing.Point(25, 159);
            this.picDonate.Name = "picDonate";
            this.picDonate.Size = new System.Drawing.Size(74, 21);
            this.picDonate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDonate.TabIndex = 12;
            this.picDonate.TabStop = false;
            this.picDonate.Click += new System.EventHandler(this.picDonate_Click);
            // 
            // lblCodestud
            // 
            this.lblCodestud.AutoSize = true;
            this.lblCodestud.Location = new System.Drawing.Point(22, 76);
            this.lblCodestud.Name = "lblCodestud";
            this.lblCodestud.Size = new System.Drawing.Size(90, 13);
            this.lblCodestud.TabIndex = 13;
            this.lblCodestud.Text = "by Edward Talbot";
            // 
            // AboutForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 194);
            this.Controls.Add(this.lblCodestud);
            this.Controls.Add(this.picDonate);
            this.Controls.Add(this.lnkID3Sharp);
            this.Controls.Add(this.lblID3Sharp);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProgramName);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		internal Button btnOK;
		internal Label lblProgramName;
		internal Label lblVersion;
		internal Label lblID3Sharp;
	    internal LinkLabel lnkID3Sharp;
	    internal PictureBox picDonate;
		internal Label lblCodestud;
	}
}
