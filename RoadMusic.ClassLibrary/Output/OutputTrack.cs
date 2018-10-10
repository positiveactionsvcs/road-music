namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// A track to be output.
    /// </summary>
	public class OutputTrack
	{
        /// <summary>
        /// Name of the track to be output.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Artist.
        /// </summary>
		public string Artist { get; set; }

        /// <summary>
        /// Track length.
        /// </summary>
		public int TrackTime { get; set; }

        /// <summary>
        /// Source file name.
        /// </summary>
		public string SourceFileName { get; set; }

        /// <summary>
        /// Destination file name.
        /// </summary>
		public string DestinationFileName { get; set; }
	}
}
