using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RoadMusic.ClassLibrary.Utility
{
    /// <summary>
    /// Utilities for dealing with file system related issues.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Return a file or folder name that is valid on the RNS-E Head Unit.
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        public static string ValidRnseFileName(string inputFileName)
        {
            // Make sure Windows is happy.
            inputFileName = ValidWindowsFileName(inputFileName);

            // Also remove exclamation marks.
            inputFileName = inputFileName.Replace("!", string.Empty);

            // Return the result.
            return inputFileName;
        }

        /// <summary>
        /// Return a file name that Windows is happy to write to disk.
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        public static string ValidWindowsFileName(string inputFileName)
        {
            // For now, use the GetInvalidFileNameChars method to eliminate characters that Windows doesn't like in filenames.
            return Path.GetInvalidFileNameChars()
                .Aggregate(inputFileName, (current, charItem) => current.Replace(charItem.ToString(), string.Empty));
        }

        /// <summary>
        /// Convert file size in Bytes to a 'friendly' value in Kb, Mb, Gb or Tb.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertSize(long bytes)
        {
            // Output a friendly file size.
            double size = bytes;
            string suffix = string.Empty;

            if (size >= 1024)
                suffix = " Kb";
            if (size >= 1048576)
                suffix = " Mb";
            if (size >= 1073741824)
                suffix = " Gb";
            if (size >= 1099511627776L)
                suffix = " Tb";

            switch (suffix)
            {
                case " Kb":
                    size = Math.Round(size/1024, 1);
                    break;

                case " Mb":
                    size = Math.Round(size/1048576, 1);
                    break;

                case " Gb":
                    size = Math.Round(size/1073741824, 1);
                    break;

                case " Tb":
                    size = Math.Round(size/1099511627776L, 1);
                    break;
            }

            return size + suffix;
        }

        /// <summary>
        /// Convert a value such as "1Mb" or "4Gb" to bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ConvertSizeMonikerToBytes(string value)
        {
            long multiplier = 0;

            // Convert anything with Kb, Mb, Gb, Tb.
            if (value.EndsWith("Kb"))
                multiplier = 1024L;

            if (value.EndsWith("Mb"))
                multiplier = 1024L * 1024L;

            if (value.EndsWith("Gb"))
                multiplier = 1024L * 1024L * 1024L;

            if (value.EndsWith("Tb"))
                multiplier = 1024L * 1024L * 1024L * 1024L;

            // Remove all alphanumeric characters which should leave just the number!
            int amount = Convert.ToInt32(Regex.Match(value, @"\d+").Value);

            return amount * multiplier;
        }
    }
}
