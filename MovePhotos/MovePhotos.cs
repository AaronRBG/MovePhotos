using System.Globalization;

namespace MovePhotos
{
    public partial class MovePhotos : Form
    {
        public MovePhotos()
        {
            InitializeComponent();
            UpdateLocalization();
            sourceFolderBrowser.RootFolder = Environment.SpecialFolder.System;
            destinationFolderBrowser.RootFolder = Environment.SpecialFolder.System;
        }

        private void UpdateLocalization()
        {
            lblSourceDirectory.Text = Properties.strings.lblSourceDirectory;
            lblDestinationDirectory.Text = Properties.strings.lblDestinationDirectory;
            lblPhotosFound.Text = Properties.strings.lblPhotosFound;
            lblMovedPhotos.Text = Properties.strings.lblMovedPhotos;
            btnSourceDirectory.Text = Properties.strings.btnSourceDirectoryName;
            btnDestinationDirectory.Text = Properties.strings.btnDestinationDirectoryName;
            btnScan.Text = Properties.strings.btnScan;
            btnMove.Text = Properties.strings.btnMove;
            btnSpanish.Image = Properties.images.ES;
            btnEnglish.Image = Properties.images.EN;
        }

        private void btnSourceDirectory_Click(object sender, EventArgs e)
        {
            sourceFolderBrowser.ShowDialog(this);
            txbSourceDirectory.Text = sourceFolderBrowser.SelectedPath.ToString();
        }
        private void btnDestinationDirectory_Click(object sender, EventArgs e)
        {
            destinationFolderBrowser.ShowDialog(this);
            txbDestinationDirectory.Text = destinationFolderBrowser.SelectedPath.ToString();
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            UpdateLocalization();
        }

        private void btnSpanish_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");
            UpdateLocalization();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnSourceDirectory.Enabled = false;
            btnDestinationDirectory.Enabled = false;
            Scan();
        }

        private void Scan()
        {
            //Do nothing
        }
    }
}
