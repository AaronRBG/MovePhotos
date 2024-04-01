using System.Globalization;

namespace MovePhotos
{
    public partial class MovePhotos : Form
    {
        private string logPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MovePhotosLog_{DateTime.Now.ToString("yyyy-MM-dd_HH_mm")}";
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MovePhotos));
        private const string DIRECTORY_SEPARATOR = "\\";
        private Dictionary<FileInfo, string> filesToMove = new Dictionary<FileInfo, string>();
        private List<string> fileExtensions = new List<string>()
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".mp4",
            ".avi"
        };

        public MovePhotos()
        {
            log4net.GlobalContext.Properties["LogFileName"] = logPath;
            InitializeComponent(); 
            UpdateLocalization();
            sourceFolderBrowser.InitialDirectory = Environment.SystemDirectory;
            destinationFolderBrowser.InitialDirectory = Environment.SystemDirectory;
            sourceFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            destinationFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
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
            txbPhotosFound.Text = filesToMove.Count.ToString();
            logger.Info(Properties.strings.lblPhotosFound + txbPhotosFound.Text);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            Move();
            DirectoryInfo directoryInfo = new DirectoryInfo(destinationFolderBrowser.SelectedPath);
            FileInfo[] filesMoved = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
            txbMovedPhotos.Text = filesMoved.Length.ToString();
            logger.Info(Properties.strings.lblMovedPhotos + txbMovedPhotos.Text);
        }

        private void Scan()
        {
            filesToMove.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(sourceFolderBrowser.SelectedPath);
            FileInfo[] fileList = directoryInfo.GetFiles();
            int current = 0;
            foreach (FileInfo file in fileList)
            {
                if (fileExtensions.Contains(file.Extension))
                {
                    DateTime timeStamp = file.LastWriteTime;
                    string destinationPath = destinationFolderBrowser.SelectedPath + DIRECTORY_SEPARATOR + timeStamp.Year.ToString() + DIRECTORY_SEPARATOR + timeStamp.ToString("MMMM", CultureInfo.CurrentUICulture);
                    filesToMove.Add(file, destinationPath);
                }

                current++;
                progressBar.Value = current / fileList.Length * 100;
            }
        }

        private new void Move()
        {
            int current = 0;
            foreach (KeyValuePair<FileInfo, string> file in filesToMove)
            {
                string directoryName = file.Value;
                string destinationPath = directoryName + DIRECTORY_SEPARATOR + file.Key.Name;

                if (!File.Exists(directoryName))
                {
                    Directory.CreateDirectory(file.Value);
                }
                if (!File.Exists(destinationPath))
                {
                    file.Key.CopyTo(file.Value + DIRECTORY_SEPARATOR + file.Key.Name);
                }
                current++;
                progressBar.Value = current / filesToMove.Count * 100;
            }
        }
    }    
}
