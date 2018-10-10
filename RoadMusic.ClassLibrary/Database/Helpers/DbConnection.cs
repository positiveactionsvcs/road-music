using System.Data.SQLite;

namespace RoadMusic.ClassLibrary.Database.Helpers
{
    /// <summary>
    /// Get connections to the database.
    /// </summary>
    public class DbConnection
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbConnectionString"></param>
        public DbConnection(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        #endregion

        #region Public Properties

        private readonly string _dbConnectionString;

        #endregion

        #region Get Database Connection

        /// <summary>
        /// Return an open connection to the database.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(_dbConnectionString);
            connection.Open();
            return connection;
        }

        #endregion

        #region Get Database Action Object

        /// <summary>
        /// Return a DbAction object that can be used to perform database operations.
        /// </summary>
        /// <returns></returns>
        public DbAction GetDbAction()
        {
            return new DbAction();
        }

        #endregion
    }
}
