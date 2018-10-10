namespace RoadMusic.ClassLibrary
{
    /// <summary>
    /// The database settings to use when accessing the local database.
    /// </summary>
	public static class DatabaseSettings
	{
		/// <summary>
        /// Database connection string.
        /// </summary>
		public static string ConnectionString { get; set; }

	    /// <summary>
        /// Path to the database file.
        /// </summary>
		public static string DatabasePath { get; set; }
	}
}
