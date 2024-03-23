namespace MovePhotos
{
    partial class MovePhotos
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovePhotos));
            sourceFolderBrowser = new FolderBrowserDialog();
            destinationFolderBrowser = new FolderBrowserDialog();
            btnSourceDirectory = new Button();
            btnDestinationDirectory = new Button();
            btnSpanish = new Button();
            btnEnglish = new Button();
            lblSourceDirectory = new Label();
            lblDestinationDirectory = new Label();
            txbSourceDirectory = new TextBox();
            txbDestinationDirectory = new TextBox();
            btnScan = new Button();
            progressBar1 = new ProgressBar();
            lblPhotosFound = new Label();
            txbPhotosFound = new TextBox();
            btnMove = new Button();
            txbMovedPhotos = new TextBox();
            lblMovedPhotos = new Label();
            SuspendLayout();
            // 
            // btnSourceDirectory
            // 
            resources.ApplyResources(btnSourceDirectory, "btnSourceDirectory");
            btnSourceDirectory.Name = "btnSourceDirectory";
            btnSourceDirectory.UseVisualStyleBackColor = true;
            btnSourceDirectory.Click += btnSourceDirectory_Click;
            // 
            // btnDestinationDirectory
            // 
            resources.ApplyResources(btnDestinationDirectory, "btnDestinationDirectory");
            btnDestinationDirectory.Name = "btnDestinationDirectory";
            btnDestinationDirectory.UseVisualStyleBackColor = true;
            btnDestinationDirectory.Click += btnDestinationDirectory_Click;
            // 
            // btnSpanish
            // 
            resources.ApplyResources(btnSpanish, "btnSpanish");
            btnSpanish.Name = "btnSpanish";
            btnSpanish.UseVisualStyleBackColor = true;
            btnSpanish.Click += btnSpanish_Click;
            // 
            // btnEnglish
            // 
            resources.ApplyResources(btnEnglish, "btnEnglish");
            btnEnglish.Name = "btnEnglish";
            btnEnglish.UseVisualStyleBackColor = true;
            btnEnglish.Click += btnEnglish_Click;
            // 
            // lblSourceDirectory
            // 
            resources.ApplyResources(lblSourceDirectory, "lblSourceDirectory");
            lblSourceDirectory.Name = "lblSourceDirectory";
            // 
            // lblDestinationDirectory
            // 
            resources.ApplyResources(lblDestinationDirectory, "lblDestinationDirectory");
            lblDestinationDirectory.Name = "lblDestinationDirectory";
            // 
            // txbSourceDirectory
            // 
            resources.ApplyResources(txbSourceDirectory, "txbSourceDirectory");
            txbSourceDirectory.Name = "txbSourceDirectory";
            txbSourceDirectory.ReadOnly = true;
            // 
            // txbDestinationDirectory
            // 
            resources.ApplyResources(txbDestinationDirectory, "txbDestinationDirectory");
            txbDestinationDirectory.Name = "txbDestinationDirectory";
            txbDestinationDirectory.ReadOnly = true;
            // 
            // btnScan
            // 
            resources.ApplyResources(btnScan, "btnScan");
            btnScan.Name = "btnScan";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // lblPhotosFound
            // 
            resources.ApplyResources(lblPhotosFound, "lblPhotosFound");
            lblPhotosFound.Name = "lblPhotosFound";
            // 
            // txbPhotosFound
            // 
            resources.ApplyResources(txbPhotosFound, "txbPhotosFound");
            txbPhotosFound.Name = "txbPhotosFound";
            txbPhotosFound.ReadOnly = true;
            // 
            // btnMove
            // 
            resources.ApplyResources(btnMove, "btnMove");
            btnMove.Name = "btnMove";
            btnMove.UseVisualStyleBackColor = true;
            // 
            // txbMovedPhotos
            // 
            resources.ApplyResources(txbMovedPhotos, "txbMovedPhotos");
            txbMovedPhotos.Name = "txbMovedPhotos";
            txbMovedPhotos.ReadOnly = true;
            // 
            // lblMovedPhotos
            // 
            resources.ApplyResources(lblMovedPhotos, "lblMovedPhotos");
            lblMovedPhotos.Name = "lblMovedPhotos";
            // 
            // MovePhotos
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txbMovedPhotos);
            Controls.Add(lblMovedPhotos);
            Controls.Add(btnMove);
            Controls.Add(txbPhotosFound);
            Controls.Add(lblPhotosFound);
            Controls.Add(progressBar1);
            Controls.Add(btnScan);
            Controls.Add(txbDestinationDirectory);
            Controls.Add(txbSourceDirectory);
            Controls.Add(lblDestinationDirectory);
            Controls.Add(lblSourceDirectory);
            Controls.Add(btnEnglish);
            Controls.Add(btnSpanish);
            Controls.Add(btnDestinationDirectory);
            Controls.Add(btnSourceDirectory);
            Name = "MovePhotos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog sourceFolderBrowser;
        private FolderBrowserDialog destinationFolderBrowser;
        private Button btnSourceDirectory;
        private Button btnDestinationDirectory;
        private Button btnSpanish;
        private Button btnEnglish;
        private Label lblSourceDirectory;
        private Label lblDestinationDirectory;
        private TextBox txbSourceDirectory;
        private TextBox txbDestinationDirectory;
        private Button btnScan;
        private ProgressBar progressBar1;
        private Label lblPhotosFound;
        private TextBox txbPhotosFound;
        private Button btnMove;
        private TextBox txbMovedPhotos;
        private Label lblMovedPhotos;
    }
}
