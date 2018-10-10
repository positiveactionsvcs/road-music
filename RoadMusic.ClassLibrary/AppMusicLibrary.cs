using System;
using System.Collections.Generic;
using Positive.TunesParser;
using RoadMusic.ClassLibrary.Database;

namespace RoadMusic.ClassLibrary
{
    /// <summary>
    /// Represents the iTunes music library.
    /// </summary>
    public static class AppMusicLibrary
    {
        #region Private Variables

        private static string _xmllibraryLocation;        
        private static Dictionary<string, Playlist> _playlistDictionary;
        //private static Dictionary<string, Track> _tracksDictionary;
        
        #endregion

        #region Music Folder & Library Handling
        
        /// <summary>
        /// Music folder as set in iTunes.
        /// </summary>
        public static string MusicFolder { get; private set; }

        /// <summary>
        /// Location of the iTunes library.
        /// </summary>
        public static string XmlLibraryLocation
        {
            get
            {
                return _xmllibraryLocation;
            }
            set
            {
                try
                {
                    // When setting the location of the iTunes Library, immediately parse in the library.
                    LoadDictionaries(value);

                    // If got to here then we're OK
                    _xmllibraryLocation = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Dictionary of playlists in the library.
        /// </summary>
        public static Dictionary<string, Playlist> PlaylistDictionary => _playlistDictionary;

        //public static Dictionary<string, Track> TracksDictionary => _tracksDictionary;

        #endregion

        #region Private Methods

        private static void LoadDictionaries(string libraryLocation)
        {
            try
            {
                TunesXmlParser parser = new TunesXmlParser(libraryLocation);

                // If here then successful parse, therefore store the music folder.
                MusicFolder = parser.MusicFolder;

                // Establish if the Library Persistent ID is the one we expect.
                Setting setting = new Setting();

                // If we haven't stored this setting yet.
                if (!setting.Find(AppSetting.LibraryPersistentId))
                {
                    setting.SettingValue = parser.LibraryPersistentId;
                }
                else
                {
                    // Detect if the existing setting is different, i.e. Library connecting to is different.
                    if (setting.SettingValue != parser.LibraryPersistentId)
                    {
                        throw new Exception("This iTunes Library is different to the one you've been using previously.  To use a different iTunes Library, delete the RoadMusic Database by choosing File > Database > Reset Database...  Then restart RoadMusic.");
                    }
                }

                if (!setting.Update(AppSetting.LibraryPersistentId))
                {
                    throw new Exception("Sorry, there was a problem trying to update the Settings database.");
                }

                // Index by PersistentId and not Id, because the XML library can change Id.
                _playlistDictionary = new Dictionary<string, Playlist>();
                //_tracksDictionary = new Dictionary<string, Track>();

                foreach (Playlist playlist in parser.Playlists.Values)
                {
                    _playlistDictionary.Add(playlist.PersistentId, playlist);
                }

                // TODO: Use Tracks Dictionary?
                //foreach (Track objTrack in parser.Tracks.Values)
                //{
                //    _tracksDictionary.Add(objTrack.Id, objTrack);
                //}
            }
            catch (Exception ex)
            {
                // Throw out our own exception instead.
                throw new Exception(string.Format("Sorry, there was a problem with reading the iTunes Music Library from '{0}'.{1}{1}{2}", libraryLocation, Environment.NewLine, ex.Message));
            }
        }

        #endregion
    }
}
