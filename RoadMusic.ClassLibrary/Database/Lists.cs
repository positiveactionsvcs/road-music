using System.Collections.Generic;
using System.Data;
using RoadMusic.ClassLibrary.Database.Helpers;

namespace RoadMusic.ClassLibrary.Database
{
    /// <summary>
    /// Retrieve lists of things.
    /// </summary>
    public class Lists : DbClass
    {
        #region GetListOfStorage

        /// <summary>
        /// Retrieve the list of storage defined.
        /// </summary>
        /// <returns></returns>
        public List<Storage> GetListOfStorage()
        {
            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();
            DataSet ds = null;

            List<Storage> listOfStorage = new List<Storage>();

            if (action.ExecDs(connection.GetConnection(), "SELECT SDCardId, SDCardDescription, SDCardSize FROM tblSDCards ORDER BY SDCardDescription", ref ds))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Storage storage = new Storage();
                        storage.ReadRecord(row);
                        listOfStorage.Add(storage);
                    }
                }

                return listOfStorage;
            }

            LastMessage = action.LastMessage;
            return null;
        }

        #endregion
    }
}
