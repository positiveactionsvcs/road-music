using System.Collections.Generic;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// A playlist to be output.
    /// </summary>
	public class OutputPlaylist
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        public OutputPlaylist()
        {
            Tracks = new List<string>();
        }

        /// <summary>
        /// Id of the playlist.
        /// </summary>
		public string Id { get; set; }

        /// <summary>
        /// Name of the playlist.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Tracks in this playlist.
        /// </summary>
		public List<string> Tracks { get; }
	}
}
