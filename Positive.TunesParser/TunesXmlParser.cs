using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;

namespace Positive.TunesParser
{
    /// <summary>
    /// Parser for the iTunes Music Library.xml file, found on any computer which has iTunes installed.
    /// </summary>
    public class TunesXmlParser
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileLocation"></param>
        public TunesXmlParser(string fileLocation)
        {
            // Create internal data structures.
            Tracks = new Dictionary<string, Track>();
            Playlists = new Dictionary<string, Playlist>();

            // Automatically parse the library.
            ParseLibrary(fileLocation);
        }

        #endregion

        #region Private Variables

        private string _originalMusicFolder;

        #endregion

        #region Public Properties

        /// <summary>
        /// The persistent Id of the whole music library.
        /// </summary>
        public string LibraryPersistentId { get; private set; }

        /// <summary>
        /// Location of the music folder.
        /// </summary>
        public string MusicFolder { get; private set; }

        /// <summary>
        /// The playlists in the library.
        /// </summary>
        public Dictionary<string, Playlist> Playlists { get; }

        // TODO: Use Tracks in library?
        /// <summary>
        /// The tracks in the library.
        /// </summary>
        private Dictionary<string, Track> Tracks { get; }

        #endregion

        #region Public Shared Methods

        /// <summary>
        /// Return default location of iTunes XML library on a Windows computer.
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLibraryLocation()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"\iTunes\iTunes Music Library.xml";
        }

        #endregion

        #region Parse

        /// <summary>
        /// Parse the contents of the iTunes Music Library.xml file into our data structure.
        /// </summary>
        /// <param name="fileLocation"></param>
        private void ParseLibrary(string fileLocation)
        {
            // Open the XML library, first as a general stream.
            using (StreamReader stream = new StreamReader(fileLocation, Encoding.GetEncoding("utf-8")))
            {
                // Then interpret it as an XML file.
                using (XmlTextReader xmlReader = new XmlTextReader(stream) { XmlResolver = null })
                {
                    XPathDocument xPathDocument = new XPathDocument(xmlReader);
                    XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
                    XPathNodeIterator nodeIterator = xPathNavigator.Select("/plist/dict");
                    nodeIterator.MoveNext();
                    nodeIterator = nodeIterator.Current.SelectChildren(XPathNodeType.All);

                    while (nodeIterator.MoveNext())
                    {
                        if (nodeIterator.Current.Value.Equals("Music Folder"))
                        {
                            if (nodeIterator.MoveNext())
                            {
                                // Parse out the location of the music folder used by the active library.
                                _originalMusicFolder = nodeIterator.Current.Value;
                                MusicFolder = _originalMusicFolder.Replace("file://localhost/", string.Empty);

                                // Strip away UNC path if it's in that format.
                                if (MusicFolder.StartsWith("/"))
                                {
                                    MusicFolder = $"/{MusicFolder}";
                                }

                                // Decode back from XML into a usable path.
                                MusicFolder = HttpUtility.UrlDecode(MusicFolder);

                                // Change forward slashes to PC backslashes.
                                MusicFolder = MusicFolder?.Replace('/', Path.DirectorySeparatorChar);
                            }
                        }
                        else if (nodeIterator.Current.Value.Equals("Library Persistent ID"))
                        {
                            if (nodeIterator.MoveNext())
                            {
                                LibraryPersistentId = nodeIterator.Current.Value;
                            }
                        }
                    }

                    // Can't move on if we don't know where the music is stored.
                    if (MusicFolder == null)
                    {
                        throw new Exception("Unable to parse Music Library element from iTunes Music Library.");
                    }

                    // This query gets us down to the point in the library that contains individual track details.
                    nodeIterator = xPathNavigator.Select("/plist/dict/dict/dict");

                    while (nodeIterator.MoveNext())
                    {
                        ParseTrack(nodeIterator.Current.SelectChildren(XPathNodeType.All));
                    }

                    // After tracks, we're looking at the playlists that are listed in the library.
                    nodeIterator = xPathNavigator.Select("/plist/dict/array/dict");

                    while (nodeIterator.MoveNext())
                    {
                        ParsePlaylist(nodeIterator.Current.SelectChildren(XPathNodeType.All));
                    }

                    xmlReader.Close();
                }
            }
        }

        /// <summary>
        /// Parse the contents of an individual playlist.
        /// </summary>
        /// <param name="nodeIterator"></param>
        private void ParsePlaylist(XPathNodeIterator nodeIterator)
        {
            // Use the Persistent ID to parse a Playlist because we want to reference Playlists by their Persistent ID in our database.
            string persistentId = null;
            string name = null;
            bool includePlaylist = true;
            List<Track> tracks = new List<Track>();

            // Iterate through this branch of this XML file until we reach the end tag.
            while (nodeIterator.MoveNext())
            {
                string currentName = nodeIterator.Current.Name;

                if (currentName.Equals("key"))
                {
                    string currentValue = nodeIterator.Current.Value;

                    if (currentValue.Equals("Master"))
                    {
                        // Exclude the "whole library" option.
                        if (nodeIterator.MoveNext())
                        {
                            if (includePlaylist)
                                includePlaylist = nodeIterator.Current.Name != "true";
                        }
                    }
                    else if (currentValue.Equals("Music"))
                    {
                        // Exclude the "all music" option.
                        if (nodeIterator.MoveNext())
                        {
                            if (includePlaylist)
                                includePlaylist = nodeIterator.Current.Name != "true";
                        }
                    }
                    else if (currentValue.Equals("Movies"))
                    {
                        // Exclude movies.
                        if (nodeIterator.MoveNext())
                        {
                            if (includePlaylist)
                                includePlaylist = nodeIterator.Current.Name != "true";
                        }
                    }
                    else if (currentValue.Equals("TV Shows"))
                    {
                        // Exclude TV shows.
                        if (nodeIterator.MoveNext())
                        {
                            if (includePlaylist)
                                includePlaylist = nodeIterator.Current.Name != "true";
                        }
                    }
                    else if (currentValue.Equals("Visible"))
                    {
                        // Exclude anything not visible.
                        if (nodeIterator.MoveNext())
                        {
                            if (includePlaylist)
                                includePlaylist = nodeIterator.Current.Name != "false";
                        }
                    }
                    else if (currentValue.Equals("Name"))
                    {
                        if (nodeIterator.MoveNext())
                            name = nodeIterator.Current.Value;

                    }
                    else if (currentValue.Equals("Playlist Persistent ID"))
                    {
                        if (nodeIterator.MoveNext())
                            persistentId = nodeIterator.Current.Value;
                    }
                }
                else if (currentName.Equals("array"))
                {
                    XPathNodeIterator trackIterator = nodeIterator.Current.Select("dict/integer");

                    while (trackIterator.MoveNext())
                    {
                        if (Tracks.ContainsKey(trackIterator.Current.Value))
                        {
                            tracks.Add(Tracks[trackIterator.Current.Value]);
                        }
                    }
                }
            }

            if (includePlaylist && persistentId != null && name != null)
            {
                Playlists.Add(persistentId, new Playlist(persistentId, name, tracks));
            }
        }

        /// <summary>
        /// Parse the details of an individual audio track.
        /// </summary>
        /// <param name="nodeIterator"></param>
        private void ParseTrack(XPathNodeIterator nodeIterator)
        {
            // Use ID (and not Persistent ID here).
            string id = null;
            string name = null;
            string artist = null;
            int trackTime = default;
            int trackSize = default;
            string location = null;
            bool inLibrary = false;
            bool disabled = false;

            // Iterate through this branch of this XML file until we reach the end tag.
            while (nodeIterator.MoveNext())
            {
                string currentValue = nodeIterator.Current.Value;

                if (currentValue.Equals("Track ID"))
                {
                    if (nodeIterator.MoveNext())
                        id = nodeIterator.Current.Value;
                }
                else if (currentValue.Equals("Name"))
                {
                    if (nodeIterator.MoveNext())
                        name = nodeIterator.Current.Value;
                }
                else if (currentValue.Equals("Artist"))
                {
                    if (nodeIterator.MoveNext())
                        artist = nodeIterator.Current.Value;
                }
                else if (currentValue.Equals("Total Time"))
                {
                    if (nodeIterator.MoveNext())
                    {
                        int.TryParse(nodeIterator.Current.Value, out trackTime);
                    }
                }
                else if (currentValue.Equals("Size"))
                {
                    if (nodeIterator.MoveNext())
                    {
                        int.TryParse(nodeIterator.Current.Value, out trackSize);
                    }
                }
                else if (currentValue.Equals("Location"))
                {
                    if (nodeIterator.MoveNext())
                    {
                        location = nodeIterator.Current.Value;
                        inLibrary = location.IndexOf(_originalMusicFolder, StringComparison.Ordinal) != -1;

                        if (inLibrary)
                        {
                            location = location.Replace(_originalMusicFolder, string.Empty);
                        }
                        else
                        {
                            location = location.Replace("file://localhost/", string.Empty);

                            // The _originalMusicFolder will have already been cleaned up to deal with 
                            // UNC paths. If we're dealing with tracks in other locations, we need to look
                            // for UNC again and clean it up. We know it's UNC if there's a slash at the front
                            // even after stripping off the localhost string above.
                            if (location.StartsWith("/"))
                            {
                                location = $"/{location}";
                            }
                        }

                        // Convert + signs to correct HTML Codes so they survive the call to UrlDecode.
                        location = location.Replace("+", "%2B");
                        location = HttpUtility.UrlDecode(location);

                        if (location.Length > 0 && location[location.Length - 1] == '/')
                        {
                            location = location.Substring(0, location.Length - 1);
                        }

                        location = location.Replace('/', Path.DirectorySeparatorChar);
                    }
                }
                else if (currentValue.Equals("Disabled"))
                {
                    disabled = true;
                }
            }

            if (id != null && name != null && location != null && location.Length > 0)
            {
                Tracks.Add(id, new Track(id, name, artist, trackTime, trackSize, location, inLibrary, disabled));
            }
        }

        #endregion
    }
}
