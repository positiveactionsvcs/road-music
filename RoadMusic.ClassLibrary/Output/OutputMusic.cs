using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using ID3Sharp;
using Positive.TunesParser;
using RoadMusic.ClassLibrary.Database;
using RoadMusic.ClassLibrary.Utility;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// Represents the output to disk.
    /// </summary>
    public class OutputMusic
    {
        #region Private Variables

        private readonly List<OutputMusicFolder> _outputMusicFolders = new List<OutputMusicFolder>();
        private readonly List<OutputPlaylist> _outputPlaylists = new List<OutputPlaylist>();

        #endregion

        #region Public Properties
        // ReSharper disable MemberCanBePrivate.Global

        /// <summary>
        /// Main output folder.
        /// </summary>
        public string OutputFolder { get; set; }

        /// <summary>
        /// Limit of the number of tracks in a folder.
        /// </summary>
        public int FolderTrackLimit { get; set; }
        
        /// <summary>
        /// Limit of the number of tracks to a playlist.
        /// </summary>
        public int PlaylistTrackLimit { get; set; }

        /// <summary>
        /// Limit to the length of the file name.
        /// </summary>
        public int FileNameLengthLimit { get; set; }

        /// <summary>
        /// Should the ID3V1 tags be stripped for the output?
        /// </summary>
        public bool StripId3V1Tags { get; set; }

        /// <summary>
        /// Should the ID3V2 tags be stripped for the output?
        /// </summary>
        public bool StripId3V2Tags { get; set; }

        // ReSharper restore MemberCanBePrivate.Global
        #endregion

        #region Public Events

        /// <summary>
        /// For documenting progress of the output.
        /// </summary>
        public Action<StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// For documenting progress of the output.
        /// </summary>
        public Action<DeleteFilesEventArgs> DeleteFiles;

        /// <summary>
        /// Event for when anything goes wrong during the output.
        /// </summary>
        public Action<OutputErrorEventArgs> OutputError;

        #endregion

        #region Private Methods

        private OutputMusicFolder GetFolder()
        {
            // If a limit is imposed on folder size...
            if (FolderTrackLimit > 0)
            {
                // Decide which folder to add track to - find first folder which is not at the folder track limit yet.
                foreach (OutputMusicFolder outputMusicFolderItem in _outputMusicFolders.Where(outputMusicFolderItem => outputMusicFolderItem.Tracks.Count < FolderTrackLimit))
                {
                    return outputMusicFolderItem;
                }

                // If got to here then all folders are full, therefore create a new one.
                OutputMusicFolder outputMusicFolder = new OutputMusicFolder();
                _outputMusicFolders.Add(outputMusicFolder);

                return outputMusicFolder;
            }

            // All music goes in the same folder.
            if (!_outputMusicFolders.Any())
            {
                // Create new one.
                OutputMusicFolder outputMusicFolder = new OutputMusicFolder();
                _outputMusicFolders.Add(outputMusicFolder);

                return outputMusicFolder;
            }

            // Return existing one.
            return _outputMusicFolders[0];
        }

        private OutputPlaylist GetPlaylist(string playlistId, string name)
        {
            // If a limit is imposed on playlist size...
            if (PlaylistTrackLimit > 0)
            {
                // Decide which playlist to add track to - find first playlist which is not at the track limit yet.
                foreach (OutputPlaylist outputPlaylistItem in _outputPlaylists.Where(outputPlaylistItem => outputPlaylistItem.Id == playlistId & outputPlaylistItem.Tracks.Count < PlaylistTrackLimit))
                {
                    return outputPlaylistItem;
                }

                // If got to here then playlist is maxed out, therefore create a new one.
                OutputPlaylist outputPlaylist = new OutputPlaylist
                {
                    Id = playlistId,
                    Name = name
                };
                _outputPlaylists.Add(outputPlaylist);

                return outputPlaylist;
            }

            // All music goes in the same playlist.
            if (!_outputPlaylists.Any())
            {
                OutputPlaylist outputPlaylist = new OutputPlaylist
                {
                    Id = playlistId,
                    Name = name
                };
                _outputPlaylists.Add(outputPlaylist);

                return outputPlaylist;
            }

            // Return existing one.
            return _outputPlaylists[0];
        }

        private OutputMusicFolder FindOutputMusicFolderByTrack(string trackId)
        {
            // Search for the Id of a Track in any of the existing folders.
            return _outputMusicFolders.FirstOrDefault(folder => folder.Tracks.ContainsKey(trackId));
        }

        private void ApplyOutputMusicFolderNames()
        {
            // Now iterate through each folder and name them.
            if (_outputMusicFolders.Count > 1)
            {
                for (int counter = 1; counter <= _outputMusicFolders.Count; counter++)
                {
                    // Label each folder as Music 1, Music 2 etc.
                    _outputMusicFolders[counter - 1].Name = "Music " + counter;
                }
            }
            else if (_outputMusicFolders.Count == 1)
            {
                _outputMusicFolders[0].Name = "Music";
            }
        }

        private void ApplyOutputPlaylistNames()
        {
            // If the long iTunes playlist is called "1980s" and has 240 tracks, it will be named as
            // Id 1: 1980s (Part 1)  - Has 99 tracks
            // Id 2: 1980s (Part 2)  - Has 99 tracks
            // Id 3: 1980s (Part 3)  - Has 42 tracks
            // etc.

            // Establish which Playlists have duplicate Ids.
            NameValueCollection duplicateIdHelper = new NameValueCollection();
            List<string> duplicateIdList = new List<string>();

            // Add all playlists to specialized collection.
            foreach (OutputPlaylist outputPlaylistItem in _outputPlaylists)
            {
                duplicateIdHelper.Add(outputPlaylistItem.Id, outputPlaylistItem.Name);
            }

            foreach (OutputPlaylist outputPlaylistItem in _outputPlaylists)
            {
                // Get a collection of all playlists with same Id as this one.
                string[] values = duplicateIdHelper.GetValues(outputPlaylistItem.Id);

                // If more than one have the same Id...
                if (values != null && values.Length > 1)
                {
                    if (!duplicateIdList.Contains(outputPlaylistItem.Id))
                    {
                        duplicateIdList.Add(outputPlaylistItem.Id);
                    }
                }
            }

            // Now iterate through and name all duplicate playlists as Part 1, Part 2 etc.
            foreach (string playlistId in duplicateIdList)
            {
                int partCounter = 1;
                var id = playlistId;

                foreach (OutputPlaylist outputPlaylistItem in _outputPlaylists.Where(outputPlaylistItem => outputPlaylistItem.Id == id))
                {
                    outputPlaylistItem.Name += " (Part " + partCounter + ")";
                    partCounter += 1;
                }
            }
        }

        private int TotalOutputTracks()
        {
            // Search for the Id of a Track in any of the existing folders.
            return _outputMusicFolders.Sum(folder => folder.Tracks.Count);
        }

        #endregion

        #region Public Methods

        #region Add Track

        /// <summary>
        /// Add a track to be output.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artist"></param>
        /// <param name="name"></param>
        /// <param name="trackTime"></param>
        /// <param name="sourceFileName"></param>
        public void AddTrack(string id, string artist, string name, int trackTime, string sourceFileName)
        {
            // Add track to this folder if not already in any of the existing music folders.
            if (FindOutputMusicFolderByTrack(id) == null)
            {
                OutputTrack outputTrack = new OutputTrack
                {
                    Artist = artist,
                    SourceFileName = sourceFileName,
                    Name = name,
                    TrackTime = trackTime
                };

                // Add to a new folder.
                GetFolder().Tracks.Add(id, outputTrack);
            }
        }

        #endregion

        #region Add Playlist

        /// <summary>
        /// Add a playlist to be output.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="playlistName"></param>
        /// <param name="tracks"></param>
        public void AddPlaylist(string playlistId, string playlistName, List<Track> tracks)
        {
            foreach (Track track in tracks.Where(track => (FindOutputMusicFolderByTrack(track.Id) != null)))
            {
                GetPlaylist(playlistId, playlistName).Tracks.Add(track.Id);
            }
        }

        #endregion

        #region File Count

        /// <summary>
        /// Calculate the total number of files that will be output when the Output method is run.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Total files = All folders + All files + All playlists... i.e. all objects of any kind.</remarks>
        public int TotalFileCount()
        {
            int totalFolders = 0;
            int totalPlaylists = 0;
            int totalTracks = 0;

            if (TotalOutputTracks() > 0)
            {
                foreach (OutputMusicFolder outputMusicFolder in _outputMusicFolders)
                {
                    totalFolders += 1;
                    totalTracks += outputMusicFolder.Tracks.Count;
                }

                // Number of playlists.
                totalPlaylists += _outputPlaylists.Count;
            }

            return (totalFolders + totalPlaylists + totalTracks);
        }

        #endregion

        #region Main Output

        /// <summary>
        /// Main output routine.
        /// </summary>
        public void Output(int storageId)
        {
            // Get correctly formed output folder.
            string outputFolderName = OutputFolder;

            if (string.IsNullOrEmpty(outputFolderName))
            {
                throw new InvalidOperationException("An output folder has not been specified.");
            }

            // Add a slash on to the output folder so that all file processing works correctly.
            if (!outputFolderName.EndsWith("\\"))
                outputFolderName += "\\";

            // Check that the output folder exists.
            if (!Directory.Exists(outputFolderName))
            {
                throw new InvalidOperationException($"The output folder '{outputFolderName}' does not exist.");
            }

            // Notify that we've started.
            StatusChangedEventArgs startArgs = new StatusChangedEventArgs(0, "Starting...", false);
            StatusChanged?.Invoke(startArgs);

            // Set up error handling so that events can be raised 
            OutputErrorEventArgs outputErrorEventArgs;

            // Find card so we know what playlists to export.
            Storage storage = new Storage();

            if (storage.Find(storageId))
            {
                // Add on name of storage.
                outputFolderName += FileSystem.ValidWindowsFileName(storage.StorageDescription);

                // If the folder doesn't exist, create it.
                if (!Directory.Exists(outputFolderName))
                {
                    try
                    {
                        Directory.CreateDirectory(outputFolderName);
                    }
                    catch (Exception ex)
                    {
                        outputErrorEventArgs = new OutputErrorEventArgs($"There was a problem creating the folder at '{outputFolderName}': {ex.Message}.");
                        OutputError?.Invoke(outputErrorEventArgs);

                        if (outputErrorEventArgs.Cancel)
                        {
                            return;
                        }
                    }
                }

                // Check if stuff exists in the folder, if it does then warn.
                DirectoryInfo outputFolder = new DirectoryInfo(outputFolderName);

                if (outputFolder.GetDirectories().Any() | outputFolder.GetFiles().Any())
                {
                    // Files and/or folders exist in the output folder therefore offer to delete them first.
                    DeleteFilesEventArgs deleteFilesEventArgs = new DeleteFilesEventArgs(outputFolderName);
                    DeleteFiles?.Invoke(deleteFilesEventArgs);

                    if (deleteFilesEventArgs.Response == DeleteFilesResponse.Yes)
                    {
                        // Delete everything in the folder (recursively).
                        foreach (DirectoryInfo directory in outputFolder.GetDirectories())
                        {
                            try
                            {
                                directory.Delete(true);
                            }
                            catch (Exception ex)
                            {
                                outputErrorEventArgs = new OutputErrorEventArgs(string.Format("Sorry, there was a problem deleting '{0}' : {1}{2}{2}", directory.FullName, ex.Message, Environment.NewLine));
                                OutputError?.Invoke(outputErrorEventArgs);

                                if (outputErrorEventArgs.Cancel)
                                {
                                    return;
                                }
                            }
                        }

                        foreach (FileInfo file in outputFolder.GetFiles())
                        {
                            try
                            {
                                file.Delete();
                            }
                            catch (Exception ex)
                            {
                                outputErrorEventArgs = new OutputErrorEventArgs(string.Format("Sorry, there was a problem deleting '{0}' : {1}{2}{2}", file.FullName, ex.Message, Environment.NewLine));
                                OutputError?.Invoke(outputErrorEventArgs);

                                if (outputErrorEventArgs.Cancel)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else if (deleteFilesEventArgs.Response == DeleteFilesResponse.Cancel)
                    {
                        // Cancel the export altogether.
                        return;
                    }
                }

                // In the output handler, the tracks needed to be added first, and then the playlists after.
                foreach (string playlistId in storage.Playlists)
                {
                    Playlist playlist = AppMusicLibrary.PlaylistDictionary[playlistId];

                    if (playlist != null)
                    {
                        // Add MP3 music.
                        foreach (Track track in playlist.AllTracks.Where(t => t.FileName.ToLower().EndsWith(".mp3")))
                        {
                            if (track.InLibrary)
                            {
                                AddTrack(track.Id, track.Artist, track.Name, track.TrackTime, AppMusicLibrary.MusicFolder + track.Location);
                            }
                            else
                            {
                                AddTrack(track.Id, track.Artist, track.Name, track.TrackTime, track.Location);
                            }
                        }

                        AddPlaylist(playlist.PersistentId, playlist.Name, playlist.AllTracks);
                    }
                }
            }

            // Check for sensible limits.
            if (FileNameLengthLimit < 64)
            {
                throw new InvalidOperationException("File name length limit must be 64 characters or more.");
            }

            if (FolderTrackLimit < 0)
            {
                throw new InvalidOperationException("Folder Track limit must be 0 or more.");
            }

            if (PlaylistTrackLimit < 0)
            {
                throw new InvalidOperationException("Playlist Track limit must be 0 or more.");
            }

            // To work out a percentage, look at the total tracks to output.
            int totalOutputTracks = TotalOutputTracks();
            int totalOutputTracksSoFar = 0;

            // Set up a counter to count number of tracks output so far.
            // Get a divisor (first 95% = the music)...
            double divisor = 95.0 / totalOutputTracks;

            if (totalOutputTracks > 0)
            {
                // Apply names, e.g. just "Music", or "Music 1", "Music 2" etc.
                ApplyOutputMusicFolderNames();

                // Deal with one music folder at a time.
                foreach (OutputMusicFolder outputMusicFolder in _outputMusicFolders)
                {
                    // Establish the name of the music folder
                    string musicFolderPath = $"{outputFolderName}\\{outputMusicFolder.Name}\\";

                    // Folder should not exist already because whole output folder should have been wiped.
                    if (!Directory.Exists(musicFolderPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(musicFolderPath);
                        }
                        catch (Exception ex)
                        {
                            outputErrorEventArgs = new OutputErrorEventArgs($"There was a problem creating the folder at '{musicFolderPath}': {ex.Message}.");
                            OutputError?.Invoke(outputErrorEventArgs);

                            // If the user pressed cancel then exit.
                            if (outputErrorEventArgs.Cancel)
                            {
                                return;
                            }
                        }
                    }

                    foreach (OutputTrack outputTrack in outputMusicFolder.Tracks.Values)
                    {
                        // Copy all the music into this folder.
                        string sourceFileName = outputTrack.SourceFileName;

                        // Eliminate any characters that the RNS-E might not agree with.
                        string destinationBaseFileName = FileSystem.ValidRnseFileName(outputTrack.Name + " - " + outputTrack.Artist);

                        // Make sure that full path in M3U is no longer than the specified limit.
                        //
                        // e.g. 64 characters minus the Folder name + slash + .mp3
                        int maximumFileNameLength = FileNameLengthLimit - (outputMusicFolder.Name.Length + 5);

                        // If necessary, truncate the base file name (i.e. the Name - Artist) to the maximum allowed.
                        if (destinationBaseFileName.Length > maximumFileNameLength)
                        {
                            destinationBaseFileName = destinationBaseFileName.Substring(0, maximumFileNameLength);
                        }

                        // Test for some files being the same name when truncated.
                        string destinationFileName = musicFolderPath + destinationBaseFileName + ".mp3";
                        int fileNameSuffixCount = 0;

                        // If the file doesn't exist already, then this loop will be skipped over.
                        while (File.Exists(destinationFileName))
                        {
                            // If file does exist there may be some tracks which are lengthy in name, but only different in the last few characters.. this may have been 
                            // truncated before we get to here.  Therefore we still need to give them a different file name by applying a "1", "2" etc. on the end.
                            fileNameSuffixCount += 1;

                            // Get a proposed file name for when the suffix is added.  Just trim off however many characters needed at the end, and add number.
                            string amendedDestinationBaseFileName = destinationBaseFileName.Substring(0, destinationBaseFileName.Length - fileNameSuffixCount.ToString().Length);

                            // Append number so now overall filename should still be no more than the limit.
                            destinationFileName = musicFolderPath + amendedDestinationBaseFileName + fileNameSuffixCount + ".mp3";
                        }

                        outputTrack.DestinationFileName = destinationFileName;

                        // Check that the source file exists before trying to copy it.
                        if (File.Exists(sourceFileName))
                        {
                            // Check whether destination file exists.  It shouldn't because of logic further up, but still...
                            if (File.Exists(outputTrack.DestinationFileName))
                            {
                                outputErrorEventArgs = new OutputErrorEventArgs($"The file '{outputTrack.DestinationFileName}' already exists.  Continue the export?");
                                OutputError?.Invoke(outputErrorEventArgs);

                                // If the user pressed cancel then exit.
                                if (outputErrorEventArgs.Cancel)
                                {
                                    return;
                                }
                            }

                            // Work out a percentage complete figure.
                            int percentProgress = Convert.ToInt32(totalOutputTracksSoFar * divisor);

                            // Check for cancellation.
                            StatusChangedEventArgs exportingFileArgs = new StatusChangedEventArgs(percentProgress, "Exporting '" + Path.GetFileName(outputTrack.DestinationFileName) + "'...", false);
                            StatusChanged?.Invoke(exportingFileArgs);

                            if (exportingFileArgs.Cancel)
                            {
                                return;
                            }

                            try
                            {
                                File.Copy(sourceFileName, outputTrack.DestinationFileName, true);
                            }
                            catch (Exception)
                            {
                                outputErrorEventArgs = new OutputErrorEventArgs("There was a problem copying from '" + sourceFileName + "' to '" + outputTrack.DestinationFileName + "'.");
                                OutputError?.Invoke(outputErrorEventArgs);

                                // If the user pressed cancel then exit.
                                if (outputErrorEventArgs.Cancel)
                                {
                                    return;
                                }
                            }

                            if (StripId3V1Tags)
                            {
                                // Should Remove ID3V1 tag for two reasons:
                                //   (a) Just in case any "helpful" Windows software comes along and tries to automatically add a V2 tag back.
                                //   (b) The description output in the M3U playlist is recognised and shown by the RNS-E on the DIS and so this makes any tag redundant anyway.
                                try
                                {
                                    ID3v1Tag.RemoveTag(outputTrack.DestinationFileName);
                                }
                                catch (Exception)
                                {
                                    // Ignore any error (e.g. if no tag there anyway).
                                }
                            }

                            if (StripId3V2Tags)
                            {
                                // Should Remove any ID3V2 tag because my RNS-E doesn't play any MP3 with this tag (contrary to the Audi instruction manual).
                                try
                                {
                                    ID3v2Tag.RemoveTag(outputTrack.DestinationFileName);
                                }
                                catch (Exception)
                                {
                                    // Ignore any error (e.g. if no tag there anyway).
                                }
                            }
                        }
                        else
                        {
                            outputErrorEventArgs = new OutputErrorEventArgs($"The file '{sourceFileName}' does not exist.");
                            OutputError?.Invoke(outputErrorEventArgs);

                            // If the user pressed cancel then exit/
                            if (outputErrorEventArgs.Cancel)
                            {
                                return;
                            }
                        }

                        // Add to value so that percentage can update properly/
                        totalOutputTracksSoFar += 1;
                    }
                }

                // Check for cancellation.
                StatusChangedEventArgs exportingPlaylistsArgs = new StatusChangedEventArgs(97, "Exporting Playlists...", false);
                StatusChanged?.Invoke(exportingPlaylistsArgs);

                if (exportingPlaylistsArgs.Cancel)
                {
                    return;
                }

                // Apply names to playlists before they are output.
                ApplyOutputPlaylistNames();

                // Create all the playlists on disk.
                foreach (OutputPlaylist outputPlaylist in _outputPlaylists)
                {
                    if (outputPlaylist.Tracks.Any())
                    {
                        // Create an M3U file for the playlist in the output folder (make sure this is OK within RNS-E rules).
                        FileStream fileM3U = null;
                        string m3UFileName = Path.Combine(outputFolderName, FileSystem.ValidRnseFileName(outputPlaylist.Name) + ".m3u");

                        try
                        {
                            fileM3U = File.Create(m3UFileName);
                        }
                        catch (Exception)
                        {
                            outputErrorEventArgs = new OutputErrorEventArgs("There was a problem creating the '" + m3UFileName + "' file.");
                            OutputError?.Invoke(outputErrorEventArgs);

                            // If the user pressed cancel then exit.
                            if (outputErrorEventArgs.Cancel)
                            {
                                return;
                            }
                        }

                        if (fileM3U != null)
                        {
                            // Open the file for writing based upon this stream.
                            StreamWriter writeM3U = new StreamWriter(fileM3U);
                            writeM3U.WriteLine("#EXTM3U");

                            foreach (string outputTrack in outputPlaylist.Tracks)
                            {
                                // Find which music folder we put the track into, in the end.
                                OutputMusicFolder trackOutputMusicFolder = FindOutputMusicFolderByTrack(outputTrack);

                                if (trackOutputMusicFolder != null && trackOutputMusicFolder.Tracks.ContainsKey(outputTrack))
                                {
                                    // Get the exact file being referred to by the Id.
                                    OutputTrack track = trackOutputMusicFolder.Tracks[outputTrack];

                                    // Write an entry in the M3U.
                                    writeM3U.WriteLine("#EXTINF:{0},{1} - {2}", track.TrackTime, track.Name, track.Artist);
                                    writeM3U.WriteLine("{0}\\{1}", trackOutputMusicFolder.Name, Path.GetFileName(track.DestinationFileName));
                                }
                            }

                            // Clean up.
                            writeM3U.Flush();
                            writeM3U.Close();
                            fileM3U.Close();
                        }
                    }
                }
            }

            StatusChanged?.Invoke(new StatusChangedEventArgs(100, "Completed.", false));
        }

        #endregion

        #endregion
    }
}
