using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RoadMusic.UI.Forms
{
	partial class StorageForm : Form
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtStorageDescription = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.cboStorageSize = new System.Windows.Forms.ComboBox();
            this.grpFileCount = new System.Windows.Forms.GroupBox();
            this.lblFileSizeWarning = new System.Windows.Forms.Label();
            this.txtTotalFileSize = new System.Windows.Forms.TextBox();
            this.lblTotalFileSize = new System.Windows.Forms.Label();
            this.lblTotalFileCount = new System.Windows.Forms.Label();
            this.lblFileLimitWarning = new System.Windows.Forms.Label();
            this.txtOverallFileCount = new System.Windows.Forms.TextBox();
            this.lblSDHC = new System.Windows.Forms.Label();
            this.lblPlaylists = new System.Windows.Forms.Label();
            this.lvPlaylists = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpFileCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(344, 578);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(429, 578);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            // 
            // txtStorageDescription
            // 
            this.txtStorageDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStorageDescription.Location = new System.Drawing.Point(15, 35);
            this.txtStorageDescription.MaxLength = 100;
            this.txtStorageDescription.Name = "txtStorageDescription";
            this.txtStorageDescription.Size = new System.Drawing.Size(407, 20);
            this.txtStorageDescription.TabIndex = 2;
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(426, 9);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "Size";
            // 
            // cboStorageSize
            // 
            this.cboStorageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStorageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStorageSize.FormattingEnabled = true;
            this.cboStorageSize.Items.AddRange(new object[] {
            "1Mb",
            "2Mb",
            "4Mb",
            "8Mb",
            "16Mb",
            "32Mb",
            "64Mb",
            "128Mb",
            "256Mb",
            "512Mb",
            "1Gb",
            "2Gb",
            "4Gb"});
            this.cboStorageSize.Location = new System.Drawing.Point(429, 35);
            this.cboStorageSize.Name = "cboStorageSize";
            this.cboStorageSize.Size = new System.Drawing.Size(78, 21);
            this.cboStorageSize.TabIndex = 3;
            this.cboStorageSize.SelectedIndexChanged += new System.EventHandler(this.cboStorageSize_SelectedIndexChanged);
            // 
            // grpFileCount
            // 
            this.grpFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFileCount.Controls.Add(this.lblFileSizeWarning);
            this.grpFileCount.Controls.Add(this.txtTotalFileSize);
            this.grpFileCount.Controls.Add(this.lblTotalFileSize);
            this.grpFileCount.Controls.Add(this.lblTotalFileCount);
            this.grpFileCount.Controls.Add(this.lblFileLimitWarning);
            this.grpFileCount.Controls.Add(this.txtOverallFileCount);
            this.grpFileCount.Location = new System.Drawing.Point(12, 468);
            this.grpFileCount.Name = "grpFileCount";
            this.grpFileCount.Size = new System.Drawing.Size(495, 104);
            this.grpFileCount.TabIndex = 7;
            this.grpFileCount.TabStop = false;
            this.grpFileCount.Text = "File Statistics";
            // 
            // lblFileSizeWarning
            // 
            this.lblFileSizeWarning.AutoSize = true;
            this.lblFileSizeWarning.ForeColor = System.Drawing.Color.Red;
            this.lblFileSizeWarning.Location = new System.Drawing.Point(231, 69);
            this.lblFileSizeWarning.Name = "lblFileSizeWarning";
            this.lblFileSizeWarning.Size = new System.Drawing.Size(213, 13);
            this.lblFileSizeWarning.TabIndex = 5;
            this.lblFileSizeWarning.Text = "Warning: This exceeds your disk space limit";
            this.lblFileSizeWarning.Visible = false;
            // 
            // txtTotalFileSize
            // 
            this.txtTotalFileSize.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalFileSize.Location = new System.Drawing.Point(139, 66);
            this.txtTotalFileSize.Name = "txtTotalFileSize";
            this.txtTotalFileSize.ReadOnly = true;
            this.txtTotalFileSize.Size = new System.Drawing.Size(76, 20);
            this.txtTotalFileSize.TabIndex = 4;
            this.txtTotalFileSize.Text = "0";
            this.txtTotalFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalFileSize
            // 
            this.lblTotalFileSize.AutoSize = true;
            this.lblTotalFileSize.Location = new System.Drawing.Point(15, 69);
            this.lblTotalFileSize.Name = "lblTotalFileSize";
            this.lblTotalFileSize.Size = new System.Drawing.Size(98, 13);
            this.lblTotalFileSize.TabIndex = 3;
            this.lblTotalFileSize.Text = "Total MP3 File Size";
            // 
            // lblTotalFileCount
            // 
            this.lblTotalFileCount.AutoSize = true;
            this.lblTotalFileCount.Location = new System.Drawing.Point(15, 33);
            this.lblTotalFileCount.Name = "lblTotalFileCount";
            this.lblTotalFileCount.Size = new System.Drawing.Size(81, 13);
            this.lblTotalFileCount.TabIndex = 0;
            this.lblTotalFileCount.Text = "Total File Count";
            // 
            // lblFileLimitWarning
            // 
            this.lblFileLimitWarning.AutoSize = true;
            this.lblFileLimitWarning.ForeColor = System.Drawing.Color.Red;
            this.lblFileLimitWarning.Location = new System.Drawing.Point(231, 33);
            this.lblFileLimitWarning.Name = "lblFileLimitWarning";
            this.lblFileLimitWarning.Size = new System.Drawing.Size(209, 13);
            this.lblFileLimitWarning.TabIndex = 2;
            this.lblFileLimitWarning.Text = "Warning: This exceeds your overall file limit";
            this.lblFileLimitWarning.Visible = false;
            // 
            // txtOverallFileCount
            // 
            this.txtOverallFileCount.BackColor = System.Drawing.SystemColors.Info;
            this.txtOverallFileCount.Location = new System.Drawing.Point(139, 30);
            this.txtOverallFileCount.Name = "txtOverallFileCount";
            this.txtOverallFileCount.ReadOnly = true;
            this.txtOverallFileCount.Size = new System.Drawing.Size(76, 20);
            this.txtOverallFileCount.TabIndex = 1;
            this.txtOverallFileCount.Text = "0";
            this.txtOverallFileCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSDHC
            // 
            this.lblSDHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDHC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSDHC.Location = new System.Drawing.Point(12, 70);
            this.lblSDHC.Name = "lblSDHC";
            this.lblSDHC.Size = new System.Drawing.Size(424, 28);
            this.lblSDHC.TabIndex = 4;
            this.lblSDHC.Text = "The RNS-E head unit is not compatible with the newer and higher capacity SDHC or " +
    "SDXC cards, only SD Cards to a maximum of 4Gb capacity.";
            // 
            // lblPlaylists
            // 
            this.lblPlaylists.AutoSize = true;
            this.lblPlaylists.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylists.Location = new System.Drawing.Point(12, 113);
            this.lblPlaylists.Name = "lblPlaylists";
            this.lblPlaylists.Size = new System.Drawing.Size(103, 13);
            this.lblPlaylists.TabIndex = 5;
            this.lblPlaylists.Text = "Playlists for inclusion";
            // 
            // lvPlaylists
            // 
            this.lvPlaylists.CheckBoxes = true;
            this.lvPlaylists.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lvPlaylists.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPlaylists.Location = new System.Drawing.Point(15, 138);
            this.lvPlaylists.Name = "lvPlaylists";
            this.lvPlaylists.Size = new System.Drawing.Size(492, 324);
            this.lvPlaylists.TabIndex = 10;
            this.lvPlaylists.UseCompatibleStateImageBehavior = false;
            this.lvPlaylists.View = System.Windows.Forms.View.Details;
            this.lvPlaylists.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPlaylists_ItemChecked);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 453;
            // 
            // StorageForm
            // 
            this.ClientSize = new System.Drawing.Size(526, 613);
            this.Controls.Add(this.lvPlaylists);
            this.Controls.Add(this.lblPlaylists);
            this.Controls.Add(this.lblSDHC);
            this.Controls.Add(this.cboStorageSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.grpFileCount);
            this.Controls.Add(this.txtStorageDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StorageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Storage Details";
            this.Load += new System.EventHandler(this.StorageForm_Load);
            this.grpFileCount.ResumeLayout(false);
            this.grpFileCount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	    internal Button btnSave;
		internal Button btnCancel;
		internal Label lblDescription;
		internal TextBox txtStorageDescription;
		internal Label lblSize;
	    internal ComboBox cboStorageSize;
		internal GroupBox grpFileCount;
		internal Label lblFileLimitWarning;
		internal TextBox txtOverallFileCount;
		internal Label lblTotalFileCount;
		internal Label lblFileSizeWarning;
		internal TextBox txtTotalFileSize;
		internal Label lblTotalFileSize;
		internal Label lblSDHC;
		internal Label lblPlaylists;
	    internal ListView lvPlaylists;
		internal ColumnHeader colName;
	}
}
