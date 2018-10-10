using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using RoadMusic.ClassLibrary.Database.Helpers;

namespace RoadMusic.ClassLibrary.Database
{
    /// <summary>
    /// Represents some storage that will be used to store outputted music.
    /// </summary>
    public class Storage : DbClass
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Storage()
        {
            Playlists = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Unique ID of the storage.
        /// </summary>
        public int StorageId { get; set; }

        /// <summary>
        /// Description of the storage.
        /// </summary>
        public string StorageDescription { get; set; }

        /// <summary>
        /// Size of the storage.
        /// </summary>
        public string StorageSize { get; set; }

        /// <summary>
        /// List of playlists on this storage.
        /// </summary>
        public List<string> Playlists { get; set; }

        #endregion

        #region Create/Read/Update/Delete

        /// <summary>
        /// Find the storage.
        /// </summary>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public bool Find(int storageId)
        {
            bool functionReturnValue = false;

            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();
            DataSet ds = null;

            if (action.ExecDs(connection.GetConnection(), "SELECT SDCardId, SDCardDescription, SDCardSize FROM tblSDCards WHERE SDCardId = " + storageId, ref ds))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ReadRecord(ds.Tables[0].Rows[0]);
                    functionReturnValue = true;
                }
            }
            else
            {
                LastMessage = action.LastMessage;
            }

            return functionReturnValue;
        }

        internal void ReadRecord(DataRow pDataRow)
        {
            StorageId = Convert.ToInt32(pDataRow["SDCardId"]);
            StorageDescription = pDataRow["SDCardDescription"].ToString();
            StorageSize = pDataRow["SDCardSize"].ToString();

            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();
            DataSet ds = null;

            if (action.ExecDs(connection.GetConnection(), "SELECT SDCardId, PlaylistId FROM tblSDCardPlaylists WHERE SDCardId = " + StorageId, ref ds))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Playlists.Add(row["PlaylistId"].ToString());
                    }
                }
            }
            else
            {
                LastMessage = action.LastMessage;
            }
        }

        /// <summary>
        /// Update the details of the storage.
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();

            bool functionReturnValue = false;

            // If a new record...
            if (StorageId == 0)
            {
                // Get a new unique ID.
                object returnValue = null;

                if (action.ExecScalar(connection.GetConnection(), "SELECT MAX(SDCardId) FROM tblSDCards", ref returnValue))
                {
                    // If table is empty, go to 1, otherwise go to the next Id value.
                    if (ReferenceEquals(returnValue, DBNull.Value))
                    {
                        StorageId = 1;
                    }
                    else
                    {
                        StorageId = Convert.ToInt32(returnValue) + 1;
                    }

                    if (action.ExecNonQuery(connection.GetConnection(), "INSERT INTO tblSDCards (SDCardId, SDCardDescription, SDCardSize) VALUES (@P0, @P1, @P2)", StorageId, StorageDescription, StorageSize))
                    {
                        functionReturnValue = true;
                    }
                    else
                    {
                        LastMessage = action.LastMessage;
                    }
                }
                else
                {
                    LastMessage = action.LastMessage;
                }
            }
            else
            {
                if (action.ExecNonQuery(connection.GetConnection(), "UPDATE tblSDCards SET SDCardDescription = @P0, SDCardSize = @P1 WHERE SDCardId = @P2", StorageDescription, StorageSize, StorageId))
                {
                    functionReturnValue = true;
                }
                else
                {
                    LastMessage = action.LastMessage;
                }
            }

            // If the update happened successfully, now insert the list of tracks.
            if (functionReturnValue)
            {
                DataSet ds = null;

                // Remove any playlists from the database that are NOT in what is to be saved.
                if (action.ExecDs(connection.GetConnection(), "SELECT SDCardId, PlaylistId FROM tblSDCardPlaylists WHERE SDCardId = " + StorageId, ref ds) && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows.Cast<DataRow>().Where(row => !Playlists.Contains(row["PlaylistId"].ToString())))
                    {
                        action.ExecNonQuery(connection.GetConnection(), "DELETE FROM tblSDCardPlaylists WHERE SDCardId = @P0 AND PlaylistId = @P1", StorageId, row["PlaylistId"].ToString());
                    }
                }

                // If playlist record does not exist, insert it.
                foreach (string playlistId in Playlists.Where(playlistId => action.ExecDs(connection.GetConnection(), "SELECT SDCardId, PlaylistId FROM tblSDCardPlaylists WHERE SDCardId = " + StorageId + " AND PlaylistId = '" + playlistId + "'", ref ds) && ds.Tables[0].Rows.Count == 0))
                {
                    action.ExecNonQuery(connection.GetConnection(), "INSERT INTO tblSDCardPlaylists (SDCardId, PlaylistId) VALUES (@P0, @P1)", StorageId, playlistId);
                }
            }

            return functionReturnValue;
        }

        /// <summary>
        /// Delete the storage.
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();

            if (!action.ExecNonQuery(connection.GetConnection(), "DELETE FROM tblSDCardPlaylists WHERE SDCardId = " + StorageId))
            {
                LastMessage = action.LastMessage;
                return false;
            }

            if (!action.ExecNonQuery(connection.GetConnection(), "DELETE FROM tblSDCards WHERE SDCardId = " + StorageId))
            {
                LastMessage = action.LastMessage;
                return false;
            }

            // Return success.
            return true;
        }

        #endregion
    }
}
