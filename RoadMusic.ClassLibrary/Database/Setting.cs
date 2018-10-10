using System.Data;
using RoadMusic.ClassLibrary.Database.Helpers;

namespace RoadMusic.ClassLibrary.Database
{
    /// <summary>
    /// A setting about the application.
    /// </summary>
    public class Setting : DbClass
    {
        #region Public Properties

        /// <summary>
        /// The value of the setting.
        /// </summary>
        public string SettingValue { get; set; }

        #endregion

        #region Create/Read/Update/Delete

        /// <summary>
        /// Find a setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool Find(AppSetting setting)
        {
            bool functionReturnValue = false;

            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();
            DataSet ds = null;

            if (action.ExecDs(connection.GetConnection(), "SELECT SettingId, SettingValue FROM tblSettings WHERE SettingId = " + setting, ref ds))
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ReadRecord(ds.Tables[0].Rows[0]);
                    // Successful read has happened
                    functionReturnValue = true;
                }
            }
            else
            {
                LastMessage = action.LastMessage;
            }

            return functionReturnValue;
        }

        private void ReadRecord(DataRow pDataRow)
        {
            SettingValue = pDataRow["SettingValue"].ToString();
        }

        /// <summary>
        /// Update a setting.
        /// </summary>
        /// <returns></returns>
        public bool Update(AppSetting setting)
        {
            // Set default value so visual studio doesn't moan
            bool functionReturnValue = false;

            DbConnection connection = new DbConnection(DatabaseSettings.ConnectionString);
            DbAction action = connection.GetDbAction();
            DataSet ds = null;

            // Establish whether we need to insert or update
            if (action.ExecDs(connection.GetConnection(), "SELECT SettingId FROM tblSettings", ref ds))
            {
                // If there is a record we need to update, otherwise insert
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (action.ExecNonQuery(connection.GetConnection(), "UPDATE tblSettings SET SettingValue = @P0 WHERE SettingId = @P1", setting, SettingValue))
                    {
                        // Insert successful
                        functionReturnValue = true;
                    }
                    else
                    {
                        LastMessage = action.LastMessage;
                    }
                }
                else
                {
                    if (action.ExecNonQuery(connection.GetConnection(), "INSERT INTO tblSettings (SettingId, SettingValue) VALUES (@P0, @P1)", setting, SettingValue))
                    {
                        // Insert successful
                        functionReturnValue = true;
                    }
                    else
                    {
                        LastMessage = action.LastMessage;
                    }
                }
            }
            else
            {
                LastMessage = action.LastMessage;
            }

            return functionReturnValue;
        }

        #endregion
    }
}
