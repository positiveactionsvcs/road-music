using System;
using System.Diagnostics;
using System.Windows.Forms;
using RoadMusic.UI.Classes;

namespace RoadMusic.UI.Forms
{
    /// <summary>
    /// The form to show information about the program.
    /// </summary>
    public partial class AboutForm
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AboutForm()
        {
            Load += AboutForm_Load;
            InitializeComponent();
        }

        #endregion

        #region Load

        /// <summary>
        /// Form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_Load(object sender, EventArgs e)
        {
            Icon = AppGlobals.ImgLib.GetIcon(AppGlobals.ImgLib.GetImageIndex(AppIcons.Music));
            lblVersion.Text = $"Version: {AppGlobals.AppVersion()}";
        }

        #endregion

        #region Links

        /// <summary>
        /// ID3 Sharp link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkID3Sharp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(lnkID3Sharp.Text);
            }
            catch (Exception)
            {
                MessageBox.Show($"Sorry, there was a problem launching {lnkID3Sharp.Text}");
            }
        }

        #endregion

        #region Donate Button

        /// <summary>
        /// You never know, someone might give me a few quid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picDonate_Click(object sender, EventArgs e)
        {
            const string payPalLink = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=eddie%40talbot%2eorg&lc=GB&item_name=RoadMusic&currency_code=GBP&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHostedGuest";

            try
            {
                Process.Start(payPalLink);
            }
            catch (Exception)
            {
                MessageBox.Show($"Sorry, there was a problem launching {payPalLink}");
            }
        }

        #endregion

        #region OK Button

        /// <summary>
        /// OK button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
