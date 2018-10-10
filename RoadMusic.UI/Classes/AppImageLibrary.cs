using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace RoadMusic.UI.Classes
{
    /// <summary>
    /// Provides a global image library to assign images to various ImageList properties.
    /// </summary>
    public class AppImageLibrary
    {
        #region Constructor and Image Population

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppImageLibrary()
        {
            // Fill the Image list and dictionary.
            Populate();
        }

        private void Populate()
        {
            // List all the embedded resources within this Assembly and identify the images.
            int counter = 0;

            foreach (string embeddedResourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                // Depending on the file extension of the embedded resource.
                switch (Path.GetExtension(embeddedResourceName))
                {
                    // Add image to dictionary so it can be looked up.
                    case ".ico":
                        _imageList.Images.Add(GetEmbeddedIcon(embeddedResourceName));
                        _imageIndex.Add(GetImageName(embeddedResourceName), counter);
                        counter += 1;

                        break;
                    case ".gif":
                    case ".bmp":
                    case ".png":
                        _imageList.Images.Add(GetEmbeddedImage(embeddedResourceName));
                        _imageIndex.Add(GetImageName(embeddedResourceName), counter);
                        counter += 1;

                        break;
                }
            }
        }

        #endregion

        #region Private Variables

        // Stores the list of images as bitmaps.
        private readonly ImageList _imageList = new ImageList();

        // Provides a lookup of image names.
        private readonly Dictionary<string, int> _imageIndex = new Dictionary<string, int>();

        #endregion

        #region Public Properties

        /// <summary>
        /// This is the main property that is used when setting an ImageList property for a ListView etc.
        /// </summary>
        public ImageList ImageList => _imageList;

        #endregion

        #region Private Functions

        private static Image GetEmbeddedImage(string name)
        {
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

            // Return the actual image object from the embedded resources.
            if (resourceStream != null)
                return Image.FromStream(resourceStream);

            return null;
        }

        private static Icon GetEmbeddedIcon(string name)
        {
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

            // Return the image as an icon from the embedded resources.
            if (resourceStream != null)
                return new Icon(resourceStream);

            return null;
        }

        private static string GetImageName(string fullResourceName)
        {
            // Return the image name without the namespace and extension.
            string[] resourceNameArray = fullResourceName.Split('.');

            if (resourceNameArray.Length <= 1)
            {
                return fullResourceName;
            }

            if (resourceNameArray.Length >= 2)
            {
                return resourceNameArray[resourceNameArray.Length - 2];
            }

            return string.Empty;
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Return the index for a given image enumeration.
        /// </summary>
        /// <param name="appIcon"></param>
        /// <returns></returns>
        public int GetImageIndex(AppIcons appIcon)
        {
            return Convert.ToInt32(_imageIndex[appIcon.ToString()]);
        }

        /// <summary>
        /// Return the image object for a given index.
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <returns></returns>
        public Image GetImage(int imageIndex)
        {
            return _imageList.Images[imageIndex];
        }

        /// <summary>
        /// Return the image object converted to an icon.
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <returns></returns>
        public Icon GetIcon(int imageIndex)
        {
            return Icon.FromHandle(((Bitmap)GetImage(imageIndex)).GetHicon());
        }

        #endregion
    }
}
