using System;
using System.IO;
using System.Windows.Forms;
using RoadMusic.ClassLibrary;
using RoadMusic.ClassLibrary.Database.Helpers;
using RoadMusic.ClassLibrary.Utility;
using RoadMusic.UI.Classes;
using RoadMusic.UI.Forms;

namespace RoadMusic.UI
{
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            // Enable nice styles in XP/Vista/7.
            Application.EnableVisualStyles();

            // Store title which can be shown in all message boxes etc.
            AppGlobals.AppName = $"{Application.ProductName} {AppGlobals.AppVersion()}";

            // Test for existence of database.
            bool databaseExists = false;
            string databasePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RoadMusic\\RoadMusic.db";

            // Set database connection string across whole app.
            DatabaseSettings.DatabasePath = databasePath;
            DatabaseSettings.ConnectionString = $"Data Source={databasePath}";

            // If database not found...
            if (!File.Exists(databasePath))
            {
                // Try to create a blank database.
                try
                {
                    DbCreate.CreateDatabase(databasePath);

                    // Database now exists.
                    databaseExists = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}  The program cannot start.", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Database already exists.
                databaseExists = true;
            }

            // Only start the app if the database exists...
            if (databaseExists)
            {
                // Set where registry access happens across whole app.
                RegistryAccess.SetSoftwareKey($"Software\\{Application.ProductName}");

                // Show the main form.
                try
                {
                    Application.Run(new MainForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sorry, a fatal error occurred : {ex.Message}", AppGlobals.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
