using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RoadMusic.UI.Properties;

namespace RoadMusic.UI.Forms
{
	partial class MainForm : Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.nmcOverallFileLimit = new System.Windows.Forms.NumericUpDown();
            this.lblOverallFileLimit = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnChooseOutputFolder = new System.Windows.Forms.Button();
            this.sspStatus = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpExportConfiguration = new System.Windows.Forms.GroupBox();
            this.btnResetSettings = new System.Windows.Forms.Button();
            this.chkStripID3V2Tags = new System.Windows.Forms.CheckBox();
            this.chkStripID3V1Tags = new System.Windows.Forms.CheckBox();
            this.lblFileNameLengthLimit = new System.Windows.Forms.Label();
            this.nmcFileNameLengthLimit = new System.Windows.Forms.NumericUpDown();
            this.lblPlaylistTrackLimit = new System.Windows.Forms.Label();
            this.nmcPlaylistTrackLimit = new System.Windows.Forms.NumericUpDown();
            this.lblFolderTrackLimit = new System.Windows.Forms.Label();
            this.nmcFolderTrackLimit = new System.Windows.Forms.NumericUpDown();
            this.btnGetDefaultLibraryLocation = new System.Windows.Forms.Button();
            this.btnChooseLibrary = new System.Windows.Forms.Button();
            this.txtLibraryLocation = new System.Windows.Forms.TextBox();
            this.bgwOutput = new System.ComponentModel.BackgroundWorker();
            this.libraryDialog = new System.Windows.Forms.OpenFileDialog();
            this.lvStorage = new System.Windows.Forms.ListView();
            this.colStorageDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStorageSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStoragePlaylistsCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpStorage = new System.Windows.Forms.GroupBox();
            this.tsStorage = new System.Windows.Forms.ToolStrip();
            this.btnAddStorage = new System.Windows.Forms.ToolStripButton();
            this.btnSelectAllStorage = new System.Windows.Forms.ToolStripButton();
            this.btnUnselectAll = new System.Windows.Forms.ToolStripButton();
            this.btnExportSelected = new System.Windows.Forms.ToolStripButton();
            this.mnuStorage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grpOutputFolder = new System.Windows.Forms.GroupBox();
            this.grpiTunesLibraryLocation = new System.Windows.Forms.GroupBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnCancelExport = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResetDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.nmcOverallFileLimit)).BeginInit();
            this.sspStatus.SuspendLayout();
            this.grpExportConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcFileNameLengthLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcPlaylistTrackLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcFolderTrackLimit)).BeginInit();
            this.grpStorage.SuspendLayout();
            this.tsStorage.SuspendLayout();
            this.grpOutputFolder.SuspendLayout();
            this.grpiTunesLibraryLocation.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // nmcOverallFileLimit
            // 
            this.nmcOverallFileLimit.Location = new System.Drawing.Point(18, 48);
            this.nmcOverallFileLimit.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nmcOverallFileLimit.Name = "nmcOverallFileLimit";
            this.nmcOverallFileLimit.Size = new System.Drawing.Size(59, 20);
            this.nmcOverallFileLimit.TabIndex = 1;
            this.nmcOverallFileLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcOverallFileLimit.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nmcOverallFileLimit.ValueChanged += new System.EventHandler(this.nmcFileLimit_ValueChanged);
            // 
            // lblOverallFileLimit
            // 
            this.lblOverallFileLimit.AutoSize = true;
            this.lblOverallFileLimit.Location = new System.Drawing.Point(15, 28);
            this.lblOverallFileLimit.Name = "lblOverallFileLimit";
            this.lblOverallFileLimit.Size = new System.Drawing.Size(211, 13);
            this.lblOverallFileLimit.TabIndex = 0;
            this.lblOverallFileLimit.Text = "Overall File Limit (Folders + Files  + Playlists)";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(18, 23);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(438, 20);
            this.txtOutputFolder.TabIndex = 0;
            // 
            // btnChooseOutputFolder
            // 
            this.btnChooseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseOutputFolder.Location = new System.Drawing.Point(466, 20);
            this.btnChooseOutputFolder.Name = "btnChooseOutputFolder";
            this.btnChooseOutputFolder.Size = new System.Drawing.Size(77, 24);
            this.btnChooseOutputFolder.TabIndex = 1;
            this.btnChooseOutputFolder.Text = "Choose...";
            this.btnChooseOutputFolder.UseVisualStyleBackColor = true;
            this.btnChooseOutputFolder.Click += new System.EventHandler(this.btnChooseOutputFolder_Click);
            // 
            // sspStatus
            // 
            this.sspStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.lblStatus});
            this.sspStatus.Location = new System.Drawing.Point(0, 603);
            this.sspStatus.Name = "sspStatus";
            this.sspStatus.Size = new System.Drawing.Size(578, 22);
            this.sspStatus.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // grpExportConfiguration
            // 
            this.grpExportConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExportConfiguration.Controls.Add(this.btnResetSettings);
            this.grpExportConfiguration.Controls.Add(this.chkStripID3V2Tags);
            this.grpExportConfiguration.Controls.Add(this.chkStripID3V1Tags);
            this.grpExportConfiguration.Controls.Add(this.lblFileNameLengthLimit);
            this.grpExportConfiguration.Controls.Add(this.nmcFileNameLengthLimit);
            this.grpExportConfiguration.Controls.Add(this.lblPlaylistTrackLimit);
            this.grpExportConfiguration.Controls.Add(this.nmcPlaylistTrackLimit);
            this.grpExportConfiguration.Controls.Add(this.lblFolderTrackLimit);
            this.grpExportConfiguration.Controls.Add(this.nmcFolderTrackLimit);
            this.grpExportConfiguration.Controls.Add(this.lblOverallFileLimit);
            this.grpExportConfiguration.Controls.Add(this.nmcOverallFileLimit);
            this.grpExportConfiguration.Location = new System.Drawing.Point(14, 73);
            this.grpExportConfiguration.Name = "grpExportConfiguration";
            this.grpExportConfiguration.Size = new System.Drawing.Size(550, 209);
            this.grpExportConfiguration.TabIndex = 1;
            this.grpExportConfiguration.TabStop = false;
            this.grpExportConfiguration.Text = "Export Configuration";
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.Location = new System.Drawing.Point(18, 170);
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.Size = new System.Drawing.Size(253, 23);
            this.btnResetSettings.TabIndex = 10;
            this.btnResetSettings.Text = "Reset to Audi RNS-E recommended settings";
            this.btnResetSettings.UseVisualStyleBackColor = true;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // chkStripID3V2Tags
            // 
            this.chkStripID3V2Tags.AutoSize = true;
            this.chkStripID3V2Tags.Checked = true;
            this.chkStripID3V2Tags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStripID3V2Tags.Location = new System.Drawing.Point(131, 137);
            this.chkStripID3V2Tags.Name = "chkStripID3V2Tags";
            this.chkStripID3V2Tags.Size = new System.Drawing.Size(106, 17);
            this.chkStripID3V2Tags.TabIndex = 9;
            this.chkStripID3V2Tags.Text = "Strip ID3 V2 tags";
            this.chkStripID3V2Tags.UseVisualStyleBackColor = true;
            this.chkStripID3V2Tags.CheckedChanged += new System.EventHandler(this.chkStripID3V2Tags_CheckedChanged);
            // 
            // chkStripID3V1Tags
            // 
            this.chkStripID3V1Tags.AutoSize = true;
            this.chkStripID3V1Tags.Checked = true;
            this.chkStripID3V1Tags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStripID3V1Tags.Location = new System.Drawing.Point(18, 137);
            this.chkStripID3V1Tags.Name = "chkStripID3V1Tags";
            this.chkStripID3V1Tags.Size = new System.Drawing.Size(106, 17);
            this.chkStripID3V1Tags.TabIndex = 8;
            this.chkStripID3V1Tags.Text = "Strip ID3 V1 tags";
            this.chkStripID3V1Tags.UseVisualStyleBackColor = true;
            this.chkStripID3V1Tags.CheckedChanged += new System.EventHandler(this.chkStripID3V1Tags_CheckedChanged);
            // 
            // lblFileNameLengthLimit
            // 
            this.lblFileNameLengthLimit.AutoSize = true;
            this.lblFileNameLengthLimit.Location = new System.Drawing.Point(220, 81);
            this.lblFileNameLengthLimit.Name = "lblFileNameLengthLimit";
            this.lblFileNameLengthLimit.Size = new System.Drawing.Size(114, 13);
            this.lblFileNameLengthLimit.TabIndex = 6;
            this.lblFileNameLengthLimit.Text = "File Name Length Limit";
            // 
            // nmcFileNameLengthLimit
            // 
            this.nmcFileNameLengthLimit.Location = new System.Drawing.Point(223, 101);
            this.nmcFileNameLengthLimit.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nmcFileNameLengthLimit.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nmcFileNameLengthLimit.Name = "nmcFileNameLengthLimit";
            this.nmcFileNameLengthLimit.Size = new System.Drawing.Size(48, 20);
            this.nmcFileNameLengthLimit.TabIndex = 7;
            this.nmcFileNameLengthLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcFileNameLengthLimit.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.nmcFileNameLengthLimit.ValueChanged += new System.EventHandler(this.nmcFileNameLengthLimit_ValueChanged);
            // 
            // lblPlaylistTrackLimit
            // 
            this.lblPlaylistTrackLimit.AutoSize = true;
            this.lblPlaylistTrackLimit.Location = new System.Drawing.Point(117, 81);
            this.lblPlaylistTrackLimit.Name = "lblPlaylistTrackLimit";
            this.lblPlaylistTrackLimit.Size = new System.Drawing.Size(94, 13);
            this.lblPlaylistTrackLimit.TabIndex = 4;
            this.lblPlaylistTrackLimit.Text = "Playlist Track Limit";
            // 
            // nmcPlaylistTrackLimit
            // 
            this.nmcPlaylistTrackLimit.Location = new System.Drawing.Point(120, 101);
            this.nmcPlaylistTrackLimit.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nmcPlaylistTrackLimit.Name = "nmcPlaylistTrackLimit";
            this.nmcPlaylistTrackLimit.Size = new System.Drawing.Size(59, 20);
            this.nmcPlaylistTrackLimit.TabIndex = 5;
            this.nmcPlaylistTrackLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcPlaylistTrackLimit.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nmcPlaylistTrackLimit.ValueChanged += new System.EventHandler(this.nmcPlaylistTrackLimit_ValueChanged);
            // 
            // lblFolderTrackLimit
            // 
            this.lblFolderTrackLimit.AutoSize = true;
            this.lblFolderTrackLimit.Location = new System.Drawing.Point(15, 81);
            this.lblFolderTrackLimit.Name = "lblFolderTrackLimit";
            this.lblFolderTrackLimit.Size = new System.Drawing.Size(91, 13);
            this.lblFolderTrackLimit.TabIndex = 2;
            this.lblFolderTrackLimit.Text = "Folder Track Limit";
            // 
            // nmcFolderTrackLimit
            // 
            this.nmcFolderTrackLimit.Location = new System.Drawing.Point(18, 101);
            this.nmcFolderTrackLimit.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nmcFolderTrackLimit.Name = "nmcFolderTrackLimit";
            this.nmcFolderTrackLimit.Size = new System.Drawing.Size(59, 20);
            this.nmcFolderTrackLimit.TabIndex = 3;
            this.nmcFolderTrackLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcFolderTrackLimit.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nmcFolderTrackLimit.ValueChanged += new System.EventHandler(this.nmcFolderTrackLimit_ValueChanged);
            // 
            // btnGetDefaultLibraryLocation
            // 
            this.btnGetDefaultLibraryLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetDefaultLibraryLocation.Location = new System.Drawing.Point(466, 16);
            this.btnGetDefaultLibraryLocation.Name = "btnGetDefaultLibraryLocation";
            this.btnGetDefaultLibraryLocation.Size = new System.Drawing.Size(77, 24);
            this.btnGetDefaultLibraryLocation.TabIndex = 2;
            this.btnGetDefaultLibraryLocation.Text = "Get Default";
            this.btnGetDefaultLibraryLocation.UseVisualStyleBackColor = true;
            this.btnGetDefaultLibraryLocation.Click += new System.EventHandler(this.btnGetDefaultLibraryLocation_Click);
            // 
            // btnChooseLibrary
            // 
            this.btnChooseLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseLibrary.Location = new System.Drawing.Point(383, 16);
            this.btnChooseLibrary.Name = "btnChooseLibrary";
            this.btnChooseLibrary.Size = new System.Drawing.Size(77, 24);
            this.btnChooseLibrary.TabIndex = 1;
            this.btnChooseLibrary.Text = "Choose...";
            this.btnChooseLibrary.UseVisualStyleBackColor = true;
            this.btnChooseLibrary.Click += new System.EventHandler(this.btnChooseLibrary_Click);
            // 
            // txtLibraryLocation
            // 
            this.txtLibraryLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLibraryLocation.Location = new System.Drawing.Point(17, 20);
            this.txtLibraryLocation.Name = "txtLibraryLocation";
            this.txtLibraryLocation.Size = new System.Drawing.Size(360, 20);
            this.txtLibraryLocation.TabIndex = 0;
            // 
            // bgwOutput
            // 
            this.bgwOutput.WorkerReportsProgress = true;
            this.bgwOutput.WorkerSupportsCancellation = true;
            this.bgwOutput.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwOutput_DoWork);
            this.bgwOutput.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwOutput_ProgressChanged);
            this.bgwOutput.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwOutput_RunWorkerCompleted);
            // 
            // libraryDialog
            // 
            this.libraryDialog.Filter = "XML files|*.xml|All files|*.*";
            // 
            // lvStorage
            // 
            this.lvStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStorage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvStorage.CheckBoxes = true;
            this.lvStorage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStorageDescription,
            this.colStorageSize,
            this.colStoragePlaylistsCount});
            this.lvStorage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvStorage.Location = new System.Drawing.Point(3, 44);
            this.lvStorage.MultiSelect = false;
            this.lvStorage.Name = "lvStorage";
            this.lvStorage.Size = new System.Drawing.Size(544, 161);
            this.lvStorage.TabIndex = 1;
            this.lvStorage.UseCompatibleStateImageBehavior = false;
            this.lvStorage.View = System.Windows.Forms.View.Details;
            this.lvStorage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvStorage_MouseClick);
            // 
            // colStorageDescription
            // 
            this.colStorageDescription.Text = "Description";
            this.colStorageDescription.Width = 274;
            // 
            // colStorageSize
            // 
            this.colStorageSize.Text = "Size";
            this.colStorageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colStorageSize.Width = 84;
            // 
            // colStoragePlaylistsCount
            // 
            this.colStoragePlaylistsCount.Text = "Number of Playlists";
            this.colStoragePlaylistsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colStoragePlaylistsCount.Width = 157;
            // 
            // grpStorage
            // 
            this.grpStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStorage.Controls.Add(this.tsStorage);
            this.grpStorage.Controls.Add(this.lvStorage);
            this.grpStorage.Location = new System.Drawing.Point(14, 288);
            this.grpStorage.Name = "grpStorage";
            this.grpStorage.Size = new System.Drawing.Size(550, 211);
            this.grpStorage.TabIndex = 2;
            this.grpStorage.TabStop = false;
            this.grpStorage.Text = "Storage";
            // 
            // tsStorage
            // 
            this.tsStorage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStorage,
            this.btnSelectAllStorage,
            this.btnUnselectAll,
            this.btnExportSelected});
            this.tsStorage.Location = new System.Drawing.Point(3, 16);
            this.tsStorage.Name = "tsStorage";
            this.tsStorage.Size = new System.Drawing.Size(544, 25);
            this.tsStorage.TabIndex = 0;
            this.tsStorage.Text = "Storage";
            // 
            // btnAddStorage
            // 
            this.btnAddStorage.Image = global::RoadMusic.UI.Properties.Resources.Add;
            this.btnAddStorage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStorage.Name = "btnAddStorage";
            this.btnAddStorage.Size = new System.Drawing.Size(101, 22);
            this.btnAddStorage.Text = "Add Storage...";
            this.btnAddStorage.ToolTipText = "Add Storage...";
            this.btnAddStorage.Click += new System.EventHandler(this.btnAddStorage_Click);
            // 
            // btnSelectAllStorage
            // 
            this.btnSelectAllStorage.Image = global::RoadMusic.UI.Properties.Resources.SelectAll;
            this.btnSelectAllStorage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAllStorage.Name = "btnSelectAllStorage";
            this.btnSelectAllStorage.Size = new System.Drawing.Size(75, 22);
            this.btnSelectAllStorage.Text = "Select All";
            this.btnSelectAllStorage.ToolTipText = "Select All";
            this.btnSelectAllStorage.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUnselectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselectAll.Image")));
            this.btnUnselectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(73, 22);
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnExportSelected
            // 
            this.btnExportSelected.Image = global::RoadMusic.UI.Properties.Resources.Export;
            this.btnExportSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportSelected.Name = "btnExportSelected";
            this.btnExportSelected.Size = new System.Drawing.Size(116, 22);
            this.btnExportSelected.Text = "Export Selected...";
            this.btnExportSelected.Click += new System.EventHandler(this.btnExportSelected_Click);
            // 
            // mnuStorage
            // 
            this.mnuStorage.Name = "mnuStorage";
            this.mnuStorage.Size = new System.Drawing.Size(61, 4);
            this.mnuStorage.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuStorage_ItemClicked);
            // 
            // grpOutputFolder
            // 
            this.grpOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOutputFolder.Controls.Add(this.txtOutputFolder);
            this.grpOutputFolder.Controls.Add(this.btnChooseOutputFolder);
            this.grpOutputFolder.Location = new System.Drawing.Point(14, 505);
            this.grpOutputFolder.Name = "grpOutputFolder";
            this.grpOutputFolder.Size = new System.Drawing.Size(550, 58);
            this.grpOutputFolder.TabIndex = 3;
            this.grpOutputFolder.TabStop = false;
            this.grpOutputFolder.Text = "Output Folder (Select a folder on your computer and not the storage itself)";
            // 
            // grpiTunesLibraryLocation
            // 
            this.grpiTunesLibraryLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpiTunesLibraryLocation.Controls.Add(this.btnChooseLibrary);
            this.grpiTunesLibraryLocation.Controls.Add(this.txtLibraryLocation);
            this.grpiTunesLibraryLocation.Controls.Add(this.btnGetDefaultLibraryLocation);
            this.grpiTunesLibraryLocation.Location = new System.Drawing.Point(14, 16);
            this.grpiTunesLibraryLocation.Name = "grpiTunesLibraryLocation";
            this.grpiTunesLibraryLocation.Size = new System.Drawing.Size(550, 51);
            this.grpiTunesLibraryLocation.TabIndex = 0;
            this.grpiTunesLibraryLocation.TabStop = false;
            this.grpiTunesLibraryLocation.Text = "iTunes Library Location (iTunes Music Library.xml)";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.Controls.Add(this.grpiTunesLibraryLocation);
            this.pnlMain.Controls.Add(this.grpStorage);
            this.pnlMain.Controls.Add(this.grpOutputFolder);
            this.pnlMain.Controls.Add(this.grpExportConfiguration);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(578, 579);
            this.pnlMain.TabIndex = 0;
            // 
            // btnCancelExport
            // 
            this.btnCancelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelExport.Location = new System.Drawing.Point(497, 605);
            this.btnCancelExport.Name = "btnCancelExport";
            this.btnCancelExport.Size = new System.Drawing.Size(67, 20);
            this.btnCancelExport.TabIndex = 2;
            this.btnCancelExport.Text = "Cancel";
            this.btnCancelExport.UseVisualStyleBackColor = true;
            this.btnCancelExport.Visible = false;
            this.btnCancelExport.Click += new System.EventHandler(this.btnCancelExport_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(578, 24);
            this.mnuMain.TabIndex = 3;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDatabase,
            this.mnuFileSeparator,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuDatabase
            // 
            this.mnuDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuResetDatabase});
            this.mnuDatabase.Name = "mnuDatabase";
            this.mnuDatabase.Size = new System.Drawing.Size(152, 22);
            this.mnuDatabase.Text = "Database";
            // 
            // mnuResetDatabase
            // 
            this.mnuResetDatabase.Name = "mnuResetDatabase";
            this.mnuResetDatabase.Size = new System.Drawing.Size(162, 22);
            this.mnuResetDatabase.Text = "Reset Database...";
            this.mnuResetDatabase.Click += new System.EventHandler(this.mnuResetDatabase_Click);
            // 
            // mnuFileSeparator
            // 
            this.mnuFileSeparator.Name = "mnuFileSeparator";
            this.mnuFileSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(152, 22);
            this.mnuAbout.Text = "&About...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(578, 625);
            this.Controls.Add(this.btnCancelExport);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.sspStatus);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.nmcOverallFileLimit)).EndInit();
            this.sspStatus.ResumeLayout(false);
            this.sspStatus.PerformLayout();
            this.grpExportConfiguration.ResumeLayout(false);
            this.grpExportConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcFileNameLengthLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcPlaylistTrackLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcFolderTrackLimit)).EndInit();
            this.grpStorage.ResumeLayout(false);
            this.grpStorage.PerformLayout();
            this.tsStorage.ResumeLayout(false);
            this.tsStorage.PerformLayout();
            this.grpOutputFolder.ResumeLayout(false);
            this.grpOutputFolder.PerformLayout();
            this.grpiTunesLibraryLocation.ResumeLayout(false);
            this.grpiTunesLibraryLocation.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	    internal NumericUpDown nmcOverallFileLimit;
		internal Label lblOverallFileLimit;
		internal FolderBrowserDialog folderBrowser;
		internal TextBox txtOutputFolder;
	    internal Button btnChooseOutputFolder;
		internal StatusStrip sspStatus;
		internal GroupBox grpExportConfiguration;
	    internal BackgroundWorker bgwOutput;
		internal TextBox txtLibraryLocation;
		internal Label lblPlaylistTrackLimit;
	    internal NumericUpDown nmcPlaylistTrackLimit;
		internal Label lblFolderTrackLimit;
	    internal NumericUpDown nmcFolderTrackLimit;
	    internal Button btnChooseLibrary;
		internal OpenFileDialog libraryDialog;
	    internal Button btnGetDefaultLibraryLocation;
		internal Label lblFileNameLengthLimit;
	    internal NumericUpDown nmcFileNameLengthLimit;
	    internal ListView lvStorage;
		internal GroupBox grpStorage;
		internal ColumnHeader colStorageDescription;
		internal ColumnHeader colStorageSize;
	    internal CheckBox chkStripID3V2Tags;
	    internal CheckBox chkStripID3V1Tags;
		internal ToolStrip tsStorage;
	    internal ToolStripButton btnAddStorage;
	    internal ContextMenuStrip mnuStorage;
		internal GroupBox grpOutputFolder;
	    internal Button btnResetSettings;
		internal GroupBox grpiTunesLibraryLocation;
		internal ToolStripProgressBar progressBar;
		internal ToolStripStatusLabel lblStatus;
		internal Panel pnlMain;
	    internal ToolStripButton btnSelectAllStorage;
	    internal ToolStripButton btnUnselectAll;
	    internal Button btnCancelExport;
		internal MenuStrip mnuMain;
		internal ToolStripMenuItem mnuFile;
	    internal ToolStripMenuItem mnuExit;
		internal ToolStripMenuItem mnuHelp;
	    internal ToolStripMenuItem mnuAbout;
		internal ColumnHeader colStoragePlaylistsCount;
	    internal ToolStripButton btnExportSelected;
		internal ToolStripMenuItem mnuDatabase;
		internal ToolStripSeparator mnuFileSeparator;
	    internal ToolStripMenuItem mnuResetDatabase;
	}
}
