using System;
using System.IO;

namespace RoadMusic.ClassLibrary.Database.Helpers
{
    /// <summary>
    /// Create the database.
    /// </summary>
    public static class DbCreate
    {
        #region Private Variables

        // Database schema for creating the database tables.
        private const string SettingsTable = "CREATE TABLE tblSettings (SettingId INT, SettingValue VARCHAR(100))";
        private const string StorageTable = "CREATE TABLE tblSDCards (SDCardId INT, SDCardDescription VARCHAR(100), SDCardSize VARCHAR(4))";
        private const string StoragePlaylistsTable = "CREATE TABLE tblSDCardPlaylists (SDCardId INT, PlaylistId VARCHAR(50))";

        #endregion

        #region Create Database

        /// <summary>
        /// Create the database with the given file name.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateDatabase(string fileName)
        {
            // Locate folder name.
            string folderName = Path.GetDirectoryName(fileName);

            // Ensure folder exists.
            if (folderName != null && !Directory.Exists(folderName))
            {
                try
                {
                    Directory.CreateDirectory(folderName);
                }
                catch (Exception)
                {
                    // Unable to create the folder in the Local App Data.
                    throw new Exception("Sorry, the folder to store the " + Path.GetFileName(fileName) + " at '" + folderName + "' could not be created.");
                }
            }

            // Try and create a blank database file.
            try
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(fileName);
            }
            catch (Exception)
            {
                throw new Exception("Sorry, the " + Path.GetFileName(fileName) + " database file could not be created.");
            }

            // Open a connection to the database.
            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            // Get a reference to our object to handle the action.
            DbAction action = connection.GetDbAction();

            // Create the Database Schema
            //
            // tblSettings
            //       SettingId int
            //       SettingValue varchar(100)
            //
            // tblSDCards
            //       SDCardId int
            //       SDCardDescription varchar(100)
            //       SDCardSize varchar(4)
            //
            // tblSDCardTracks
            //       SDCardId int
            //       TrackId varchar(50)
            //
            if (!action.ExecNonQuery(connection.GetConnection(), SettingsTable))
            {
                throw new Exception(action.LastMessage);
            }

            if (!action.ExecNonQuery(connection.GetConnection(), StorageTable))
            {
                throw new Exception(action.LastMessage);
            }

            if (!action.ExecNonQuery(connection.GetConnection(), StoragePlaylistsTable))
            {
                throw new Exception(action.LastMessage);
            }
        }

        #endregion
    }
}
