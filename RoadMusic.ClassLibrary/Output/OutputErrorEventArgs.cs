using System;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// If an error occurrs during the output process.
    /// </summary>
	public class OutputErrorEventArgs : EventArgs
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        public OutputErrorEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; private set; }

		/// <summary>
		/// Does the user want to continue despite the error?
		/// </summary>
		public bool Cancel { get; set; }
	}
}
