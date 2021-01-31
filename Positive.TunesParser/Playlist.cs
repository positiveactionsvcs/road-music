using System.Collections.Generic;

namespace Positive.TunesParser
{
    /// <summary>
    /// Represents an individual Playlist from the iTunes library.
    /// </summary>
    public class Playlist
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="persistentId"></param>
        /// <param name="name"></param>
        /// <param name="tracks"></param>
        public Playlist(string persistentId, string name, List<Track> tracks)
        {
            PersistentId = persistentId;
            Name = name;
            AllTracks = tracks;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The persistent ID for this playlist.
        /// </summary>
        public string PersistentId { get; }

        /// <summary>
        /// The display name for this playlist.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of all the tracks that appear within this playlist.
        /// </summary>
        public List<Track> AllTracks { get; }

        // TODO: Use Enabled Tracks?
        ///// <summary>
        ///// A list of just the enabled tracks that appear within this playlist.
        ///// </summary>
        //public List<Track> EnabledTracks
        //{
        //    get
        //    {
        //        return AllTracks.Where(track => !track.Disabled).ToList();
        //    }
        //}

        #endregion
    }
}
