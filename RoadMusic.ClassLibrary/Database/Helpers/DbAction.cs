using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace RoadMusic.ClassLibrary.Database.Helpers
{
    /// <summary>
    /// Take actions on the database, i.e. executing commands.
    /// </summary>
    public class DbAction
    {
        #region Public Properties

        /// <summary>
        /// Collection of messages/errors returned from stored procedure calls.
        /// </summary>
        private List<DbMessage> Messages { get; set; }

        /// <summary>
        /// The last message returned.
        /// </summary>
        public string LastMessage
        {
            get
            {
                if (Messages.Any())
                {
                    // There may be many but return the first one.
                    return Messages[0].Message;
                }

                return null;
            }
        }

        #endregion

        #region ExecDs

        /// <summary>
        /// Execute a command which returns a DataSet.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="outputDs"></param>
        /// <returns></returns>
        public bool ExecDs(SQLiteConnection connection, string commandText, ref DataSet outputDs)
        {
            return prv_ExecDs(connection, commandText, 30, ref outputDs, null);
        }

        private bool prv_ExecDs(SQLiteConnection connection, string commandText, int commandTimeout, ref DataSet outputDs, params object[] parameters)
        {
            bool functionReturnValue = false;

            // Initialise a new set of messages.
            Messages = new List<DbMessage>();

            if ((connection != null))
            {
                if (connection.State == ConnectionState.Closed | connection.State == ConnectionState.Broken)
                {
                    Messages.Add(new DbMessage($"Connection {connection.State}"));
                }
                else
                {
                    SQLiteCommand tempCommand = new SQLiteCommand { CommandTimeout = commandTimeout };

                    try
                    {
                        // Prepare command object and attach the given parameters.
                        PrepareCommand(ref tempCommand, ref connection, commandText);
                        AttachParameters(ref tempCommand, parameters);

                        // Create a Data Adapter which is necessary for creating a DataSet.
                        SQLiteDataAdapter dapAdapter = new SQLiteDataAdapter(tempCommand);
                        outputDs = new DataSet();

                        // Put this in the Try..Catch block because of any potential errors.
                        dapAdapter.Fill(outputDs);

                        // Clean up.
                        dapAdapter.Dispose();

                        // Set success to true, barring any raiserror messages which can set result to False again.
                        functionReturnValue = true;
                    }
                    catch (Exception ex)
                    {
                        outputDs = null;
                        Messages.Add(new DbMessage(ex.Message));
                    }
                    finally
                    {
                        // Release all objects.
                        tempCommand.Dispose();
                    }
                }
            }
            else
            {
                Messages.Add(new DbMessage("Connection Is Nothing"));
            }

            return functionReturnValue;
        }

        #endregion

        #region ExecNonQuery

        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public bool ExecNonQuery(SQLiteConnection connection, string commandText)
        {
            return prv_ExecNonQuery(connection, commandText, 30, null);
        }

        /// <summary>
        /// Execute a command that takes parameters.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecNonQuery(SQLiteConnection connection, string commandText, params object[] parameters)
        {
            return prv_ExecNonQuery(connection, commandText, 30, parameters);
        }

        private bool prv_ExecNonQuery(SQLiteConnection connection, string commandText, int commandTimeout, params object[] parameters)
        {
            bool functionReturnValue = false;

            // Initialise a new set of messages.
            Messages = new List<DbMessage>();

            if ((connection != null))
            {
                if (connection.State == ConnectionState.Closed | connection.State == ConnectionState.Broken)
                {
                    Messages.Add(new DbMessage($"Connection {connection.State}"));
                }
                else
                {
                    SQLiteCommand tempCommand = new SQLiteCommand { CommandTimeout = commandTimeout };

                    try
                    {
                        // Prepare command object and attach the given parameters.
                        PrepareCommand(ref tempCommand, ref connection, commandText);
                        AttachParameters(ref tempCommand, parameters);

                        // Put this in the Try..Catch block because of any potential errors.
                        tempCommand.ExecuteNonQuery();

                        // Set success to true, barring any raiserror messages which can set result to False again.
                        functionReturnValue = true;
                    }
                    catch (Exception ex)
                    {
                        Messages.Add(new DbMessage(ex.Message));
                    }
                    finally
                    {
                        // Release all objects.
                        tempCommand.Dispose();
                    }
                }
            }
            else
            {
                Messages.Add(new DbMessage("Connection Is Nothing"));
            }

            return functionReturnValue;
        }

        #endregion

        #region ExecScalar

        /// <summary>
        /// Execute a command which returns a value.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public bool ExecScalar(SQLiteConnection connection, string commandText, ref object returnValue)
        {
            return prv_ExecScalar(connection, commandText, 30, ref returnValue, null);
        }

        private bool prv_ExecScalar(SQLiteConnection connection, string commandText, int commandTimeout, ref object returnValue, params object[] parameters)
        {
            bool functionReturnValue = false;

            // Initialise a new set of messages.
            Messages = new List<DbMessage>();

            if ((connection != null))
            {
                if (connection.State == ConnectionState.Closed | connection.State == ConnectionState.Broken)
                {
                    Messages.Add(new DbMessage($"Connection {connection.State}"));
                }
                else
                {
                    SQLiteCommand tempCommand = new SQLiteCommand { CommandTimeout = commandTimeout };

                    try
                    {
                        // Prepare command object and attach the given parameters.
                        PrepareCommand(ref tempCommand, ref connection, commandText);
                        AttachParameters(ref tempCommand, parameters);

                        // Put this in the Try..Catch block because of any potential errors.
                        returnValue = tempCommand.ExecuteScalar();

                        // Set success to true, barring any raiserror messages which can set result to False again.
                        functionReturnValue = true;
                    }
                    catch (Exception ex)
                    {
                        Messages.Add(new DbMessage(ex.Message));
                    }
                    finally
                    {
                        // Release all objects.
                        tempCommand.Dispose();
                    }
                }
            }
            else
            {
                Messages.Add(new DbMessage("Connection Is Nothing"));
            }

            return functionReturnValue;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Attach the given parameters to the command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        private void AttachParameters(ref SQLiteCommand command, params object[] parameters)
        {
            // If some parameters exist...
            if (parameters != null)
            {
                // Keep a counter to keep track of @P0, @P1 etc.
                int parameterCount = 0;

                // Go through each one and determine its data type.
                foreach (object param in parameters)
                {
                    // Determine what type of parameter this is and set the correct type accordingly.
                    SQLiteParameter sqlLiteParam = new SQLiteParameter("@P" + parameterCount);

                    // Add more in here if necessary.
                    if (param is string)
                    {
                        sqlLiteParam.Value = param.ToString();
                    }

                    if (param is int)
                    {
                        sqlLiteParam.Value = Convert.ToInt32(param);
                    }

                    command.Parameters.Add(sqlLiteParam);
                    parameterCount += 1;
                }
            }
        }

        /// <summary>
        /// Configure the command object with the specified parameters.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        private static void PrepareCommand(ref SQLiteCommand command, ref SQLiteConnection connection, string commandText)
        {
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
        }

        #endregion
    }
}
