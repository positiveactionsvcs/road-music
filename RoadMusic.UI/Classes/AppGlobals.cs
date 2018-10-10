using System;
using System.Reflection;

namespace RoadMusic.UI.Classes
{
    /// <summary>
    /// Global variables.
    /// </summary>
    public static class AppGlobals
    {
        #region Private Variables

        private static AppImageLibrary _imgLib;

        #endregion

        #region Application Name & Version

        /// <summary>
        /// Name of the application.
        /// </summary>
        public static string AppName { get; set; }

        /// <summary>
        /// Version of the application.
        /// </summary>
        /// <returns></returns>
        public static string AppVersion()
        {
            // Retrieve the version number of the current Assembly for display purposes.
            Version versionNumber = Assembly.GetExecutingAssembly().GetName().Version;

            // Make sure that version returned doesn't have any extraneous "0" on the end
            // e.g.  if internal version number is 2.2.1.0, then return 2.2.1
            //       if internal version number is 2.2.0.0 then return 2.2
            //       etc..
            //
            if (versionNumber.Revision > 0)
            {
                // Return all four digits.
                return versionNumber.ToString();
            }

            // Revision not filled in.
            if (versionNumber.Build > 0)
            {
                // Return first three digits.
                return $"{versionNumber.Major}.{versionNumber.Minor}.{versionNumber.Build}";
            }

            // Build is also not filled in, therefore always return first two digits.
            return $"{versionNumber.Major}.{versionNumber.Minor}";
        }

        #endregion

        #region Image Library

        /// <summary>
        /// Image library.
        /// </summary>
        public static AppImageLibrary ImgLib => _imgLib ?? (_imgLib = new AppImageLibrary());

        #endregion
    }
}
