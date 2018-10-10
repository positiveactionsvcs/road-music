using System;

namespace RoadMusic.ClassLibrary.Output
{
    /// <summary>
    /// If an error occurrs during the output process.
    /// </summary>
	public class StatusChangedEventArgs : EventArgs
	{
        /// <summary>
        /// Constructor.
        /// </summary>
        public StatusChangedEventArgs(int percentProgress, string statusMessage, bool cancel)
        {
            PercentProgress = percentProgress;
            StatusMessage = statusMessage;
            Cancel = cancel;
        }

        /// <summary>
        /// Percentage progress through the operation.
        /// </summary>
        public int PercentProgress { get; }

        /// <summary>
        /// Message about what is happening.
        /// </summary>
        public string StatusMessage { get; }

        /// <summary>
        /// Does the user want to cancel?
        /// </summary>
        public bool Cancel { get; set; }
	}
}
