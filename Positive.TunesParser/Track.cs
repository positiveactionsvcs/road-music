using System;
using System.IO;

namespace Positive.TunesParser
{
    /// <summary>
    /// Represents an individual audio track from the iTunes library.
    /// </summary>
    public class Track
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="artist"></param>
        /// <param name="trackTime"></param>
        /// <param name="trackSize"></param>
        /// <param name="location"></param>
        /// <param name="inLibrary"></param>
        /// <param name="disabled"></param>
        public Track(string id, string name, string artist, int trackTime, int trackSize, string location, bool inLibrary, bool disabled)
        {
            Id = id;
            Name = name;
            Artist = artist;
            _trackTime = trackTime;
            TrackSize = trackSize;
            Location = location;
            InLibrary = inLibrary;
            Disabled = disabled;
        }

        #endregion

        #region Private Variables

        private readonly int _trackTime;

        #endregion

        #region Public Properties

        /// <summary>
        /// The ID for this track.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The display name for this track.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The filename for this track, without the full path.
        /// </summary>
        public string FileName
        {
            get
            {
                int index = Location.LastIndexOf(Path.DirectorySeparatorChar);

                return index == -1 ? Location : Location.Substring(index + 1);
            }
        }

        /// <summary>
        /// The artist performing this track.
        /// </summary>
        public string Artist { get; }

        /// <summary>
        /// The duration of the track, in seconds.
        /// </summary>
        public int TrackTime => Convert.ToInt32(_trackTime / 1000);

        /// <summary>
        /// The size of the track, in bytes.
        /// </summary>
        public int TrackSize { get; }

        /// <summary>
        /// The complete path and filename on disc for this track.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Indicates whether this track is located in the location managed by the iTunes library.
        /// </summary>
        public bool InLibrary { get; }

        /// <summary>
        /// Indicates whether this track is unticked and will not be synced to the iDevice.
        /// </summary>
        // TODO: Reinstate when know what to do with unticked tracks.
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public bool Disabled { get; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global

        #endregion
    }
}
