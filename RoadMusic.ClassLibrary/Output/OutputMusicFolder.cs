using System.Collections.Generic;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// A folder containing music.
    /// </summary>
	public class OutputMusicFolder
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        public OutputMusicFolder()
        {
            Tracks = new Dictionary<string, OutputTrack>();
        }

        /// <summary>
        /// Name of the folder.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// The tracks inside this folder.
        /// </summary>
		public Dictionary<string, OutputTrack> Tracks { get; }
	}
}
