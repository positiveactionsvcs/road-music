using System;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// If files/folders need to be deleted.
    /// </summary>
	public class DeleteFilesEventArgs : EventArgs
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        public DeleteFilesEventArgs(string folderName)
        {
            FolderName = folderName;
        }

        /// <summary>
        /// The folder or file to be deleted.
        /// </summary>
        public string FolderName { get; }

        /// <summary>
        /// What does the user want to do?
        /// </summary>
        public DeleteFilesResponse Response { get; set; }
	}
}
