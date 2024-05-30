using System.Globalization;

namespace MovePhotos
{
    public partial class MovePhotos : Form
    {
        private string logPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\MovePhotosLog_{DateTime.Now.ToString("yyyy-MM-dd_HH_mm")}";
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MovePhotos));
        private const string DIRECTORY_SEPARATOR = "\\";
        private int filesOriginalDestinationCount;
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
            lblSelectedPhotos.Text = Properties.strings.lblSelectedPhotos;
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
            if (sourceFolderBrowser.SelectedPath != string.Empty && destinationFolderBrowser.SelectedPath != string.Empty)
            {
                btnScan.Enabled = true;
            }
        }
        private void btnDestinationDirectory_Click(object sender, EventArgs e)
        {
            destinationFolderBrowser.ShowDialog(this);
            txbDestinationDirectory.Text = destinationFolderBrowser.SelectedPath.ToString();
            if (sourceFolderBrowser.SelectedPath != string.Empty && destinationFolderBrowser.SelectedPath != string.Empty)
            {
                btnScan.Enabled = true;
            }
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
            Scan();
            txbPhotosFound.Text = filesToMove.Count.ToString();
            btnMove.Enabled = true;
            logger.Info(Properties.strings.lblSourceDirectory + txbSourceDirectory.Text);
            logger.Info(Properties.strings.lblPhotosFound + filesToMove.Count);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            btnSourceDirectory.Enabled = false;
            btnDestinationDirectory.Enabled = false;
            btnScan.Enabled = false;
            progressBar.Value = 0;
            Move();
            DirectoryInfo directoryInfo = new DirectoryInfo(destinationFolderBrowser.SelectedPath);
            FileInfo[] filesAfter = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Where(file => fileExtensions.Contains(file.Extension)).ToArray();
            int filesMoved = filesAfter.Length - filesOriginalDestinationCount;
            txbMovedPhotos.Text = filesMoved.ToString();
            logger.Info(Properties.strings.lblDestinationDirectory + txbDestinationDirectory.Text);
            logger.Info(Properties.strings.lblSelectedPhotos + txbSelectedPhotos.Text);
            logger.Info(Properties.strings.lblMovedPhotos + filesMoved);

            if (filesMoved < filesToMove.Count)
            {
                Verify();
            }
        }

        private void Scan()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sourceFolderBrowser.SelectedPath);
            FileInfo[] fileList = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Where(file => fileExtensions.Contains(file.Extension)).ToArray();

            PopulateTreeView(sourceFolderBrowser.SelectedPath);
            filesToMove.Clear();
            PrepareSelectedPhotos(treeView.Nodes);

            directoryInfo = new DirectoryInfo(destinationFolderBrowser.SelectedPath);
            FileInfo[] filesBefore = directoryInfo.GetFiles("*", SearchOption.AllDirectories).Where(file => fileExtensions.Contains(file.Extension)).ToArray();
            filesOriginalDestinationCount = filesBefore.Length;
        }
        private new void Move()
        {
            int current = 0;
            foreach (KeyValuePair<FileInfo, string> file in filesToMove)
            {
                string directoryName = file.Value;
                string destinationPath = directoryName + DIRECTORY_SEPARATOR + file.Key.Name;

                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(file.Value);
                }
                if (!File.Exists(destinationPath))
                {
                    try
                    {
                        file.Key.CopyTo(file.Value + DIRECTORY_SEPARATOR + file.Key.Name);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                    }
                }
                current++;
                progressBar.Value = current / filesToMove.Count * 100;
            }
        }

        private void Verify()
        {
            foreach (KeyValuePair<FileInfo, string> file in filesToMove)
            {
                string directoryName = file.Value;
                string destinationPath = directoryName + DIRECTORY_SEPARATOR + file.Key.Name;

                if (!Directory.Exists(directoryName))
                {
                    logger.Error(Properties.strings.TheDirectory + directoryName + Properties.strings.WasNotCreated);
                }
                if (!File.Exists(destinationPath))
                {
                    logger.Error(Properties.strings.TheFile + file.Key.Name + Properties.strings.WasNotCopied);
                }
            }
        }

        private void PopulateTreeView(string rootFolder)
        {
            treeView.Nodes.Clear();
            TreeNode rootNode = new TreeNode(rootFolder);
            rootNode.Tag = rootFolder;
            rootNode.Text = $"{rootFolder} ({GetFileCount(rootFolder)})";
            rootNode.Checked = true;
            treeView.Nodes.Add(rootNode);
            PopulateSubDirectories(rootNode);
        }

        private void PopulateSubDirectories(TreeNode node)
        {
            string folderPath = (string)node.Tag;
            try
            {
                string[] subDirectories = Directory.GetDirectories(folderPath);
                foreach (string subDirectory in subDirectories)
                {
                    TreeNode subNode = new TreeNode(subDirectory);
                    subNode.Tag = subDirectory;
                    subNode.Text = $"{Path.GetFileName(subDirectory)} ({GetFileCount(subDirectory)})";
                    subNode.Checked = true;
                    node.Nodes.Add(subNode);
                    PopulateSubDirectories(subNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle the exception if access is denied to a folder
            }
        }

        private int GetFileCount(string folderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                return directoryInfo.GetFiles("*", SearchOption.AllDirectories).Where(file => fileExtensions.Contains(file.Extension)).Count();
            }
            catch (UnauthorizedAccessException)
            {
                // Handle the exception if access is denied to a folder
                return 0;
            }
        }

        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            filesToMove.Clear();
            PrepareSelectedPhotos(treeView.Nodes);
            txbSelectedPhotos.Text = filesToMove.Count().ToString();
        }

        private void PrepareSelectedPhotos(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(node.Tag.ToString());
                    FileInfo[] fileList = directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly).Where(file => fileExtensions.Contains(file.Extension)).ToArray();
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
                PrepareSelectedPhotos(node.Nodes);
            }
        }

    }
}
