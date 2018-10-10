using System;
using System.Collections.Generic;
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
    /// Details of the storage, e.g. an SD Card, USB drive etc, and what playlists will be exported to it.
    /// </summary>
    public partial class StorageForm
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Internal ID of the storage card.
        /// </summary>
        public int StorageId { get; set; }

        /// <summary>
        /// Description of the storage.
        /// </summary>
        public string StorageDescription
        {
            get { return txtStorageDescription.Text; }
            set { txtStorageDescription.Text = value; }
        }

        /// <summary>
        /// Size of the storage.
        /// </summary>
        public string StorageSize
        {
            get { return cboStorageSize.SelectedItem.ToString(); }
            set { cboStorageSize.SelectedItem = value; }
        }

        /// <summary>
        /// The list of playlists that are currently selected.
        /// </summary>
        public List<string> Playlists => (from ListViewItem playlistItem in lvPlaylists.Items where playlistItem.Checked select playlistItem.Name).ToList();

        #endregion

        #region Load

        /// <summary>
        /// Form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StorageForm_Load(object sender, EventArgs e)
        {
            // Setup the form.
            Icon = AppGlobals.ImgLib.GetIcon(AppGlobals.ImgLib.GetImageIndex(AppIcons.Storage));
            lvPlaylists.Items.Clear();

            // Populate the iTunes playlists.
            foreach (Playlist playlistItem in AppMusicLibrary.PlaylistDictionary.Values)
            {
                lvPlaylists.Items.Add(playlistItem.PersistentId, playlistItem.Name, -1);
            }

            // Populate the existing storage details if in edit mode.
            if (StorageId > 0)
            {
                PopulateStorage();
            }
        }

        #endregion

        #region Playlists

        /// <summary>
        /// Populate the playlists on the storage.
        /// </summary>
        private void PopulateStorage()
        {
            Storage storage = new Storage();

            if (storage.Find(StorageId))
            {
                foreach (string playlistId in storage.Playlists)
                {
                    ListViewItem[] playlistViewItems = lvPlaylists.Items.Find(playlistId, false);

                    if (playlistViewItems.Length > 0)
                    {
                        foreach (ListViewItem playlistNode in playlistViewItems)
                        {
                            playlistNode.Checked = true;
                        }
                    }
                }

                // Show total files that would be output.
                UpdateFileCount();
                UpdateTotalFileSize();
            }
            else
            {
                MessageBox.Show(storage.LastMessage);
            }
        }

        #endregion

        #region ListView

        /// <summary>
        /// When a playlist has been checked or unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPlaylists_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // Show total files that would be output.
            UpdateFileCount();
            UpdateTotalFileSize();
        }

        #endregion

        #region Save Button

        /// <summary>
        /// Save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Lists lists = new Lists();
            List<Storage> listOfStorage = lists.GetListOfStorage();

            // Check that this name isn't already in use.
            foreach (Storage storage in listOfStorage)
            {
                if (storage.StorageDescription == txtStorageDescription.Text & storage.StorageId != StorageId)
                {
                    MessageBox.Show($"Sorry, the description '{storage.StorageDescription}' has already been taken.  Type in another description.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }

            // Check for a blank description.
            if (string.IsNullOrEmpty(txtStorageDescription.Text.Trim()))
            {
                MessageBox.Show(@"Please enter a description, e.g. My 1980s Tracks", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Check for a blank size.
                if (cboStorageSize.SelectedItem == null)
                {
                    MessageBox.Show(@"Please select the size of this storage.  The size of the storage in Mb (Megabytes) or Gb (Gigabytes) might be printed on the SD Card or USB drive.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        #endregion

        #region Update File Count

        /// <summary>
        /// Get how how many files will be exported.
        /// </summary>
        private void UpdateFileCount()
        {
            Cursor = Cursors.WaitCursor;

            // Instantiate main logic.
            OutputMusic outputMusic = new OutputMusic
            {
                FolderTrackLimit = RegistryAccess.FolderTrackLimit,
                PlaylistTrackLimit = RegistryAccess.PlaylistTrackLimit
            };

            // Get all selected playlists.
            List<Playlist> playlists = Playlists.Select(playlistId => AppMusicLibrary.PlaylistDictionary[playlistId]).ToList();

            foreach (Playlist playlistItem in playlists)
            {
                // Add MP3 tracks.
                foreach (Track track in playlistItem.AllTracks.Where(track => track.FileName.ToLower().EndsWith(".mp3")))
                {
                    if (track.InLibrary)
                    {
                        outputMusic.AddTrack(track.Id, track.Artist, track.Name, track.TrackTime, AppMusicLibrary.MusicFolder + track.Location);
                    }
                    else
                    {
                        outputMusic.AddTrack(track.Id, track.Artist, track.Name, track.TrackTime, track.Location);
                    }
                }

                // Add playlist.
                outputMusic.AddPlaylist(playlistItem.PersistentId, playlistItem.Name, playlistItem.AllTracks);
            }

            // Retrieve the file count this would generate.
            int totalFileCount = outputMusic.TotalFileCount();

            // Display a warning if the total count is over the given limit.
            lblFileLimitWarning.Visible = (totalFileCount > RegistryAccess.OverallFileLimit);

            // Display the amount.
            txtOverallFileCount.Text = outputMusic.TotalFileCount().ToString();

            Cursor = Cursors.Default;
        }

        #endregion

        #region Update Total File Size

        /// <summary>
        /// Keep track of total size (in bytes).
        /// </summary>
        private void UpdateTotalFileSize()
        {
            Cursor = Cursors.WaitCursor;

            // Get all selected playlists.
            List<Playlist> playlists = Playlists.Select(playlistId => AppMusicLibrary.PlaylistDictionary[playlistId]).ToList();

            // Add music in those playlists.
            long totalFileSize = (from playlistItem in playlists from track in playlistItem.AllTracks where track.FileName.ToLower().EndsWith(".mp3") select track).Aggregate(default(long), (current, track) => current + track.TrackSize);

            // Display the amount.
            txtTotalFileSize.Text = FileSystem.ConvertSize(totalFileSize);

            // If a size is selected...
            if (cboStorageSize.SelectedIndex != -1)
            {
                // Translate the storage size into a figure in bytes to compare with.
                long size = FileSystem.ConvertSizeMonikerToBytes(cboStorageSize.SelectedItem.ToString());

                // Display a warning if the total count is over the given limit.
                lblFileSizeWarning.Visible = (totalFileSize > size);
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the size of the storage changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStorageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotalFileSize();
        }

        #endregion
    }
}
