using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Positive.TunesParser;
using RoadMusic.ClassLibrary;
using RoadMusic.ClassLibrary.Database;
using RoadMusic.ClassLibrary.Output;
using RoadMusic.ClassLibrary.Utility;
using RoadMusic.UI.Classes;

namespace RoadMusic.UI.Forms
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;

            // Set flag so that we can prevent events messing anything up.
            _isPopulating = true;

            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            _isPopulating = false;
        }

        #endregion

        #region Private Variables

        // Main logic which needs setting up to catch events.
        private OutputMusic _outputMusic;

        // Flag to store if form is being initialized.
        private readonly bool _isPopulating;

        // Flag to store collection of storage cards for multiple export.
        private readonly List<int> _exportMultipleStorageIdList = new List<int>();

        #endregion

        #region Private Constants

        // Constants to define the limitations of the RNS-E head unit.
        private const int RnsEOverallfilelimit = 512;
        private const int RnsEFoldertracklimit = 256;
        private const int RnsEPlaylisttracklimit = 99;
        private const int RnsEFilenamelengthlimit = 64;
        private const bool RnsEStripid3V1Tags = true;
        private const bool RnsEStripid3V2Tags = true;

        #endregion

        #region Load

        /// <summary>
        /// Form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set up the form.
            Text = AppGlobals.AppName;
            Icon = AppGlobals.ImgLib.GetIcon(AppGlobals.ImgLib.GetImageIndex(AppIcons.Music));
            lvStorage.SmallImageList = AppGlobals.ImgLib.ImageList;

            // Hide progress bar until it is used during the export process.
            progressBar.Visible = false;

            // Populate default or stored values.
            nmcOverallFileLimit.Value = RegistryAccess.OverallFileLimit == int.MinValue ? RnsEOverallfilelimit : RegistryAccess.OverallFileLimit;
            nmcFolderTrackLimit.Value = RegistryAccess.FolderTrackLimit == int.MinValue ? RnsEFoldertracklimit : RegistryAccess.FolderTrackLimit;
            nmcPlaylistTrackLimit.Value = RegistryAccess.PlaylistTrackLimit == int.MinValue ? RnsEPlaylisttracklimit : RegistryAccess.PlaylistTrackLimit;
            nmcFileNameLengthLimit.Value = RegistryAccess.FileNameLengthLimit == int.MinValue ? RnsEFilenamelengthlimit : RegistryAccess.FileNameLengthLimit;
            chkStripID3V1Tags.Checked = RegistryAccess.StripId3V1Tags;
            chkStripID3V2Tags.Checked = RegistryAccess.StripId3V2Tags;

            // Other output settings.
            txtOutputFolder.Text = RegistryAccess.OutputFolder;

            // Populate the list of storage currently defined.
            PopulateStorage();

            // Load library from stored location in the Registry.
            string libraryLocation = RegistryAccess.LibraryLocation;

            // If there is no iTunes library, try and find the library at the default location.
            if (string.IsNullOrEmpty(libraryLocation))
            {
                if (LoadDefaultLibraryLocation())
                {
                    MessageBox.Show($"RoadMusic has automatically loaded your iTunes library at: '{AppMusicLibrary.XmlLibraryLocation}'", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(@"To get started, click ""Add Storage..."" and choose the Playlists you want to export.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EnableControls();
                }
                else
                {
                    MessageBox.Show(@"RoadMusic could not find your iTunes library at its default location.  Please browse to it manually.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisableControls();
                }
            }
            else
            {
                if (LoadLibrary(libraryLocation))
                {
                    EnableControls();
                }
                else
                {
                    DisableControls();
                }
            }
        }

        #endregion

        #region iTunes Library

        /// <summary>
        /// Choose the iTunes Library button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseLibrary_Click(object sender, EventArgs e)
        {
            if (libraryDialog.ShowDialog() == DialogResult.OK)
            {
                // Pass in the file name just selected.
                if (LoadLibrary(libraryDialog.FileName))
                {
                    EnableControls();
                }
                else
                {
                    DisableControls();
                }
            }
        }

        /// <summary>
        /// Get Default iTunes library button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDefaultLibraryLocation_Click(object sender, EventArgs e)
        {
            LoadDefaultLibraryLocation();
        }

        /// <summary>
        /// Get the default iTunes library.
        /// </summary>
        /// <returns></returns>
        private bool LoadDefaultLibraryLocation()
        {
            // Establish where the local library should be.
            string defaultLibraryLocation = TunesXmlParser.GetDefaultLibraryLocation();

            if (File.Exists(defaultLibraryLocation))
            {
                if (LoadLibrary(defaultLibraryLocation))
                {
                    EnableControls();
                    return true;
                }

                DisableControls();
                return false;
            }

            MessageBox.Show($"An iTunes Library file could not be found in the default location of '{defaultLibraryLocation}'.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        /// <summary>
        /// Load an iTunes library.
        /// </summary>
        /// <param name="libraryLocation"></param>
        /// <returns></returns>
        private bool LoadLibrary(string libraryLocation)
        {
            if (!string.IsNullOrEmpty(libraryLocation))
            {
                // Store the location of the successfully read library in the Registry.
                RegistryAccess.LibraryLocation = libraryLocation;

                // Attempt to change the location of the library.
                try
                {
                    // Set the location in AppGlobals which attempts to load and parse the library.
                    AppMusicLibrary.XmlLibraryLocation = libraryLocation;

                    // Update UI with our new library.
                    txtLibraryLocation.Text = libraryLocation;

                    // Return success
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return false;
        }

        #endregion

        #region Changed Settings (Registry)

        private void nmcFileLimit_ValueChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.OverallFileLimit = Convert.ToInt32(nmcOverallFileLimit.Value);
            }
        }

        private void nmcFolderTrackLimit_ValueChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.FolderTrackLimit = Convert.ToInt32(nmcFolderTrackLimit.Value);
            }
        }

        private void nmcPlaylistTrackLimit_ValueChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.PlaylistTrackLimit = Convert.ToInt32(nmcPlaylistTrackLimit.Value);
            }
        }

        private void nmcFileNameLengthLimit_ValueChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.FileNameLengthLimit = Convert.ToInt32(nmcFileNameLengthLimit.Value);
            }
        }

        private void chkStripID3V1Tags_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.StripId3V1Tags = chkStripID3V1Tags.Checked;
            }
        }

        private void chkStripID3V2Tags_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isPopulating)
            {
                RegistryAccess.StripId3V2Tags = chkStripID3V2Tags.Checked;
            }
        }

        #endregion

        #region Reset to RNS-E Settings Button

        /// <summary>
        /// Restore recommended settings for the RNS-E.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            nmcOverallFileLimit.Value = RnsEOverallfilelimit;
            nmcFolderTrackLimit.Value = RnsEFoldertracklimit;
            nmcPlaylistTrackLimit.Value = RnsEPlaylisttracklimit;
            nmcFileNameLengthLimit.Value = RnsEFilenamelengthlimit;
            chkStripID3V1Tags.Checked = RnsEStripid3V1Tags;
            chkStripID3V2Tags.Checked = RnsEStripid3V2Tags;
        }

        #endregion

        #region Choose Output Folder Button

        /// <summary>
        /// Choose Output Folder button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseOutputFolder_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = @"Browse for the folder where all files will be output.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowser.SelectedPath;
                RegistryAccess.OutputFolder = txtOutputFolder.Text;
            }
        }

        #endregion

        #region Storage

        /// <summary>
        /// Populate the list of defined storage.
        /// </summary>
        private void PopulateStorage()
        {
            lvStorage.Items.Clear();

            Lists lists = new Lists();
            List<Storage> listOfStorage = lists.GetListOfStorage();

            if (listOfStorage != null)
            {
                foreach (Storage storage in listOfStorage)
                {
                    ListViewItem lvItem = lvStorage.Items.Add(storage.StorageDescription);
                    lvItem.SubItems.Add(storage.StorageSize);
                    lvItem.SubItems.Add(storage.Playlists.Count.ToString());
                    lvItem.ImageIndex = AppGlobals.ImgLib.GetImageIndex(AppIcons.Storage);

                    // Set name so know what to pull from database.
                    lvItem.Name = storage.StorageId.ToString();
                }
            }
            else
            {
                MessageBox.Show(lists.LastMessage, AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Add new storage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStorage_Click(object sender, EventArgs e)
        {
            using (StorageForm storageForm = new StorageForm())
            {
                if (storageForm.ShowDialog() == DialogResult.OK)
                {
                    Storage storage = new Storage
                    {
                        StorageDescription = storageForm.StorageDescription,
                        StorageSize = storageForm.StorageSize,
                        Playlists = storageForm.Playlists
                    };

                    if (storage.Update())
                    {
                        PopulateStorage();
                    }
                }
            }
        }

        /// <summary>
        /// A storage device has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvStorage_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvStorage.SelectedIndices.Count > 0 && e.Button == MouseButtons.Right)
            {
                // Configure and show the context menu.
                mnuStorage.Items.Clear();
                mnuStorage.Items.Add("Edit...", AppGlobals.ImgLib.GetImage(AppGlobals.ImgLib.GetImageIndex(AppIcons.Playlist)));
                mnuStorage.Items.Add("Export...", AppGlobals.ImgLib.GetImage(AppGlobals.ImgLib.GetImageIndex(AppIcons.Storage)));
                mnuStorage.Items.Add("Delete...", AppGlobals.ImgLib.GetImage(AppGlobals.ImgLib.GetImageIndex(AppIcons.Delete)));
                mnuStorage.Show(lvStorage, e.X, e.Y);
            }
        }

        #region Select All/Unselect All

        /// <summary>
        /// Go through all the listed storage and check it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvStorage.Items)
            {
                lvItem.Checked = true;
            }
        }

        /// <summary>
        /// Uncheck all of the storage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvStorage.Items)
            {
                lvItem.Checked = false;
            }
        }

        #endregion

        #region Edit/Export/Delete

        private void mnuStorage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Hide the menu before anything else happens
            mnuStorage.Hide();

            switch (e.ClickedItem.Text)
            {
                case "Edit...":
                    Storage storage = new Storage();

                    if (storage.Find(Convert.ToInt32(lvStorage.SelectedItems[0].Name)))
                    {
                        StorageForm storageForm = new StorageForm
                        {
                            StorageId = storage.StorageId,
                            StorageDescription = storage.StorageDescription,
                            StorageSize = storage.StorageSize
                        };

                        if (storageForm.ShowDialog() == DialogResult.OK)
                        {
                            storage.StorageId = storageForm.StorageId;
                            storage.StorageDescription = storageForm.StorageDescription;
                            storage.StorageSize = storageForm.StorageSize;
                            storage.Playlists = storageForm.Playlists;

                            if (storage.Update())
                            {
                                // Change the information in the ListView.
                                lvStorage.SelectedItems[0].Text = storage.StorageDescription;
                                lvStorage.SelectedItems[0].SubItems[1].Text = storage.StorageSize;
                                lvStorage.SelectedItems[0].SubItems[2].Text = storage.Playlists.Count.ToString();
                            }
                        }
                    }

                    break;

                case "Export...":
                    if (MessageBox.Show(@"Are you sure you want to do this export?", AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ExportStorage(Convert.ToInt32(lvStorage.SelectedItems[0].Name));
                    }

                    break;

                case "Delete...":
                    if (MessageBox.Show(@"Are you sure you want to delete this storage?", AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Storage storageToDelete = new Storage();

                        if (storageToDelete.Find(Convert.ToInt32(lvStorage.SelectedItems[0].Name)))
                        {
                            if (storageToDelete.Delete())
                            {
                                lvStorage.Items.Remove(lvStorage.SelectedItems[0]);
                            }
                            else
                            {
                                MessageBox.Show(@"Sorry, this could not be deleted.\n\n" + storageToDelete.LastMessage, AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    break;
            }
        }

        #endregion

        #endregion

        #region Export

        /// <summary>
        /// Do the main export.
        /// </summary>
        /// <param name="storageId"></param>
        private void ExportStorage(int storageId)
        {
            // Instantiate main logic to configure how the music will be output.
            _outputMusic = new OutputMusic
            {
                FolderTrackLimit = Convert.ToInt32(nmcFolderTrackLimit.Value),
                PlaylistTrackLimit = Convert.ToInt32(nmcPlaylistTrackLimit.Value),
                FileNameLengthLimit = Convert.ToInt32(nmcFileNameLengthLimit.Value),
                StripId3V1Tags = chkStripID3V1Tags.Checked,
                StripId3V2Tags = chkStripID3V2Tags.Checked,
                OutputFolder = txtOutputFolder.Text
            };
            _outputMusic.OutputError += _outputMusic_OutputError;
            _outputMusic.DeleteFiles += _outputMusic_DeleteFiles;
            _outputMusic.StatusChanged += _outputMusic_StatusChanged;

            // Run the main operations asychronously.
            bgwOutput.RunWorkerAsync(storageId);
        }

        #endregion

        #region Export Selected Button

        /// <summary>
        /// Export selected button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportSelected_Click(object sender, EventArgs e)
        {
            if (lvStorage.CheckedItems.Count > 0)
            {
                // Alter grammar depending on how many items are checked.
                string message = string.Empty;

                if (lvStorage.CheckedItems.Count == 1)
                {
                    message = "Are you sure you want to export this?";
                }
                else if (lvStorage.CheckedItems.Count > 1)
                {
                    message = "Are you sure you want to export these?  This may take a few minutes.";
                }

                if (MessageBox.Show(message, AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    // Show message reminding about sub-folders.
                    if (lvStorage.CheckedItems.Count > 1)
                    {
                        MessageBox.Show(@"Please note, that because you are exporting multiple items, a subfolder with the name of each storage device will be created in the Output Folder.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Gather the list of Storage Ids to output and run the first one.
                    _exportMultipleStorageIdList.Clear();

                    foreach (ListViewItem lvItem in lvStorage.CheckedItems)
                    {
                        _exportMultipleStorageIdList.Add(Convert.ToInt32(lvItem.Name));
                    }

                    if (_exportMultipleStorageIdList.Any())
                    {
                        ExportStorage(_exportMultipleStorageIdList[0]);
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Please tick at least one storage device in the list.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Asynchronous Output Background Worker

        /// <summary>
        /// Do the main asynchronous work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwOutput_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _outputMusic.Output(Convert.ToInt32(e.Argument));

                // If got to here OK then export completed without any fatal error.
                e.Result = new OutputResultEventArgs(Convert.ToInt32(e.Argument), "Completed");
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        /// <summary>
        /// Update status bar with message passed through from the ReportProgress command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwOutput_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Prevent user from spawning multiple threads and update the rest of the UI.
            if (e.ProgressPercentage == 0)
            {
                pnlMain.Enabled = false;
                progressBar.Visible = true;
                btnCancelExport.Visible = true;
            }

            progressBar.Value = e.ProgressPercentage;
            lblStatus.Text = e.UserState.ToString();
        }

        /// <summary>
        /// The asynchronous work has been completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwOutput_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Flag to store if controls will be returned (this won't happen if in the middle of a multiple export).
            bool returnUiControl = false;

            // Get result.
            OutputResultEventArgs args = e.Result as OutputResultEventArgs;

            if (args != null)
            {
                OutputResultEventArgs outputResult = args;

                if (outputResult.Message == "Completed")
                {
                    // If a multiple export is happening, remove the one we just finished.
                    if (_exportMultipleStorageIdList.Contains(outputResult.StorageId))
                    {
                        _exportMultipleStorageIdList.Remove(outputResult.StorageId);
                    }

                    // Kick off the next one.
                    if (_exportMultipleStorageIdList.Any())
                    {
                        ExportStorage(_exportMultipleStorageIdList[0]);
                    }
                    else
                    {
                        // Everything has finished.
                        returnUiControl = true;

                        if (progressBar.Value == 100)
                        {
                            MessageBox.Show(@"All exports have been completed.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    returnUiControl = true;
                    MessageBox.Show(outputResult.Message, AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                returnUiControl = true;
                MessageBox.Show(e.Result.ToString(), AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            if (returnUiControl)
            {
                // Make everything available again.
                pnlMain.Enabled = true;
                progressBar.Visible = false;
                btnCancelExport.Visible = false;
                lblStatus.Text = @"Ready";
            }
        }

        #region Event Handling

        /// <summary>
        /// Status of the output has changed.
        /// </summary>
        /// <param name="e"></param>
        private void _outputMusic_StatusChanged(StatusChangedEventArgs e)
        {
            // If the user wants to cancel then make sure this is passed through to the routine which can quit it.
            e.Cancel = bgwOutput.CancellationPending;

            // Call ReportProgress method so that UI can be updated.
            // When using the BackgroundWorker, always try IsBusy before ReportProgress to avoid a potential exception.
            if (bgwOutput.IsBusy)
                bgwOutput.ReportProgress(e.PercentProgress, e.StatusMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void _outputMusic_DeleteFiles(DeleteFilesEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                $"There are already files in the '{e.FolderName}' folder.  Do you want to delete these first?", AppGlobals.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                e.Response = DeleteFilesResponse.Yes;
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Response = DeleteFilesResponse.No;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                e.Response = DeleteFilesResponse.Cancel;
            }
        }

        /// <summary>
        /// Error occurred during the output.
        /// </summary>
        /// <param name="e"></param>
        private void _outputMusic_OutputError(OutputErrorEventArgs e)
        {
            e.Cancel = MessageBox.Show(e.Message + @"  Would you like to continue?", AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK;
        }

        #endregion

        #endregion

        #region Cancel Export Button

        /// <summary>
        /// Cancel export button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to cancel the export?", AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // If in multiple make sure this is cancelled as well.
                _exportMultipleStorageIdList.Clear();
                bgwOutput.CancelAsync();
            }
        }

        #endregion

        #region Drop-Down Menus

        /// <summary>
        /// Reset the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuResetDatabase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("The database is currently located at: '{0}'.{1}{1}If you choose to delete the database, all your saved configuration will be deleted.  RoadMusic will immediately close.", DatabaseSettings.DatabasePath, Environment.NewLine), AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    File.Delete(DatabaseSettings.DatabasePath);

                    // Need to close because database isn't there anymore
                    MessageBox.Show(@"Database deleted successfully.  RoadMusic will now close.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sorry, there was a problem trying to delete the database file: {ex.Message}.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// About.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        #endregion

        #region Enable/Disable UI

        /// <summary>
        /// Enable the UI.
        /// </summary>
        private void EnableControls()
        {
            grpExportConfiguration.Enabled = true;
            grpOutputFolder.Enabled = true;
            grpStorage.Enabled = true;
            btnExportSelected.Enabled = true;
        }

        /// <summary>
        /// Disable the UI.
        /// </summary>
        private void DisableControls()
        {
            grpExportConfiguration.Enabled = false;
            grpOutputFolder.Enabled = false;
            grpStorage.Enabled = false;
            btnExportSelected.Enabled = false;
        }

        #endregion

        #region Close

        /// <summary>
        /// Form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bgwOutput.IsBusy)
            {
                e.Cancel = (MessageBox.Show(@"Export is in progress.  Are you sure you want to close?", AppGlobals.AppName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel);
            }
        }

        #endregion
    }
}
