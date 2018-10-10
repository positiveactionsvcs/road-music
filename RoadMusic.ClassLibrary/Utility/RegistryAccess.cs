using System;
using Microsoft.Win32;

namespace RoadMusic.ClassLibrary.Utility
{
    /// <summary>
    /// Class for setting or retrieving registry settings.
    /// </summary>
    public static class RegistryAccess
    {
        private static string _softwareKey;

        #region Registry Key

        /// <summary>
        /// Registry key used to store settings.
        /// </summary>
        public static void SetSoftwareKey(string softwareKey)
        {
            _softwareKey = softwareKey;
        }

        #endregion

        #region Library Location

        /// <summary>
        /// Location of the iTunes library.
        /// </summary>
        public static string LibraryLocation
        {
            get
            {
                string returnValue = null;
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("LibraryLocation");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("LibraryLocation") == RegistryValueKind.String)
                    {
                        returnValue = value.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }

                    keyReg.Close();
                }

                return returnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("LibraryLocation", value);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Overall File Limit

        /// <summary>
        /// Total number of files that can be written.
        /// </summary>
        public static int OverallFileLimit
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                int returnValue = default(int);

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("OverallFileLimit");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("OverallFileLimit") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            returnValue = Convert.ToInt32(value);
                        }
                        catch
                        {
                            returnValue = int.MinValue;
                        }
                    }
                    else
                    {
                        returnValue = int.MinValue;
                    }

                    keyReg.Close();
                }

                return returnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("OverallFileLimit", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Folder Track Limit

        /// <summary>
        /// Maximum number of tracks that can be in a folder.
        /// </summary>
        public static int FolderTrackLimit
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                int returnValue = default(int);

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("FolderTrackLimit");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("FolderTrackLimit") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            returnValue = Convert.ToInt32(value);
                        }
                        catch
                        {
                            returnValue = int.MinValue;
                        }
                    }
                    else
                    {
                        returnValue = int.MinValue;
                    }

                    keyReg.Close();
                }

                return returnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("FolderTrackLimit", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Playlist Track Limit

        /// <summary>
        /// Maximum number of tracks that can be in a playlist.
        /// </summary>
        public static int PlaylistTrackLimit
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                int returnValue = default(int);

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("PlaylistTrackLimit");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("PlaylistTrackLimit") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            returnValue = Convert.ToInt32(value);
                        }
                        catch
                        {
                            returnValue = int.MinValue;
                        }
                    }
                    else
                    {
                        returnValue = int.MinValue;
                    }

                    keyReg.Close();
                }

                return returnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("PlaylistTrackLimit", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region File Name Length Limit

        /// <summary>
        /// Maximum number of characters in a file name.
        /// </summary>
        public static int FileNameLengthLimit
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                int returnValue = default(int);
 
                if (keyReg != null)
                {
                    object value = keyReg.GetValue("FileNameLengthLimit");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("FileNameLengthLimit") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            returnValue = Convert.ToInt32(value);
                        }
                        catch
                        {
                            returnValue = int.MinValue;
                        }
                    }
                    else
                    {
                        returnValue = int.MinValue;
                    }

                    keyReg.Close();
                }

                return returnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("FileNameLengthLimit", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Strip ID3V1 Tags

        /// <summary>
        /// Strip the ID3V1 tags.
        /// </summary>
        public static bool StripId3V1Tags
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                bool boolean = false;

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("StripID3V1Tags");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("StripID3V1Tags") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            boolean = Convert.ToBoolean(keyReg.GetValue("StripID3V1Tags", false));
                        }
                        catch
                        {
                            boolean = false;
                        }
                    }

                    keyReg.Close();
                }

                return boolean;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("StripID3V1Tags", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Strip ID3V2 Tags

        /// <summary>
        /// Strip the ID3V2 tags.
        /// </summary>
        public static bool StripId3V2Tags
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                bool boolean = false;

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("StripID3V2Tags");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("StripID3V2Tags") == RegistryValueKind.DWord)
                    {
                        try
                        {
                            boolean = Convert.ToBoolean(keyReg.GetValue("StripID3V2Tags", false));
                        }
                        catch
                        {
                            boolean = false;
                        }
                    }

                    keyReg.Close();
                }

                return boolean;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("StripID3V2Tags", value, RegistryValueKind.DWord);
                    keyReg.Close();
                }
            }
        }

        #endregion

        #region Output Folder

        /// <summary>
        /// Folder to output to.
        /// </summary>
        public static string OutputFolder
        {
            get
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);
                string functionReturnValue = null;

                if (keyReg != null)
                {
                    object value = keyReg.GetValue("OutputFolder");

                    // Return the value if it's the correct type.
                    if (value != null && keyReg.GetValueKind("OutputFolder") == RegistryValueKind.String)
                    {
                        functionReturnValue = value.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }

                    keyReg.Close();
                }

                return functionReturnValue;
            }
            set
            {
                RegistryKey keyReg = Registry.CurrentUser.CreateSubKey(_softwareKey);

                if (keyReg != null)
                {
                    keyReg.SetValue("OutputFolder", value);
                    keyReg.Close();
                }
            }
        }

        #endregion
    }
}
