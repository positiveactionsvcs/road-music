namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// The response to whether a user wants to delete files/folders before exporting.
    /// </summary>
    public enum DeleteFilesResponse
    {
        /// <summary>
        /// They want to delete.
        /// </summary>
        Yes,

        /// <summary>
        /// Nope they don't want to delete.
        /// </summary>
        No,

        /// <summary>
        /// Cancel the whole thing.
        /// </summary>
        Cancel
    }
}
