using System;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// The result of the output.
    /// </summary>
	public class OutputResultEventArgs : EventArgs
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="message"></param>
        public OutputResultEventArgs(int storageId, string message)
        {
            StorageId = storageId;
            Message = message;
        }

        /// <summary>
        /// The storage that has been written to.
        /// </summary>
        public int StorageId { get; private set; }

        /// <summary>
        /// A message relating to the finished output.
        /// </summary>
		public string Message { get; private set; }
	}
}
