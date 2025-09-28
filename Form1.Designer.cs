namespace BackupPhotosVideos
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.lblDestinyFolder = new System.Windows.Forms.Label();
            this.btnChooseSourceFolder = new System.Windows.Forms.Button();
            this.btnChooseDestinyFolder = new System.Windows.Forms.Button();
            this.tbSourceFolder = new System.Windows.Forms.TextBox();
            this.tbDestinyFolder = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.lbPhotos = new System.Windows.Forms.Label();
            this.lbVideos = new System.Windows.Forms.Label();
            this.lbPhotosCounter = new System.Windows.Forms.Label();
            this.lbVideosCounter = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.copyProgressBar = new System.Windows.Forms.ProgressBar();
            this.tbLogErrors = new System.Windows.Forms.TextBox();
            this.btnVerifyIntegrity = new System.Windows.Forms.Button();
            this.tbLogIntegrity = new System.Windows.Forms.TextBox();
            this.integrityProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnPauseCopy = new System.Windows.Forms.Button();
            this.btnResumeCopy = new System.Windows.Forms.Button();
            this.checkBoxIncludeYear = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeMonth = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSourceFolder
            // 
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Location = new System.Drawing.Point(13, 27);
            this.lblSourceFolder.Name = "lblSourceFolder";
            this.lblSourceFolder.Size = new System.Drawing.Size(76, 13);
            this.lblSourceFolder.TabIndex = 0;
            this.lblSourceFolder.Text = "Source Folder:";
            // 
            // lblDestinyFolder
            // 
            this.lblDestinyFolder.AutoSize = true;
            this.lblDestinyFolder.Location = new System.Drawing.Point(13, 59);
            this.lblDestinyFolder.Name = "lblDestinyFolder";
            this.lblDestinyFolder.Size = new System.Drawing.Size(77, 13);
            this.lblDestinyFolder.TabIndex = 1;
            this.lblDestinyFolder.Text = "Destiny Folder:";
            // 
            // btnChooseSourceFolder
            // 
            this.btnChooseSourceFolder.Location = new System.Drawing.Point(511, 22);
            this.btnChooseSourceFolder.Name = "btnChooseSourceFolder";
            this.btnChooseSourceFolder.Size = new System.Drawing.Size(147, 23);
            this.btnChooseSourceFolder.TabIndex = 2;
            this.btnChooseSourceFolder.Text = "Choose";
            this.btnChooseSourceFolder.UseVisualStyleBackColor = true;
            this.btnChooseSourceFolder.Click += new System.EventHandler(this.btnChooseSourceFolder_Click);
            // 
            // btnChooseDestinyFolder
            // 
            this.btnChooseDestinyFolder.Location = new System.Drawing.Point(511, 54);
            this.btnChooseDestinyFolder.Name = "btnChooseDestinyFolder";
            this.btnChooseDestinyFolder.Size = new System.Drawing.Size(147, 23);
            this.btnChooseDestinyFolder.TabIndex = 3;
            this.btnChooseDestinyFolder.Text = "Choose";
            this.btnChooseDestinyFolder.UseVisualStyleBackColor = true;
            this.btnChooseDestinyFolder.Click += new System.EventHandler(this.btnChooseDestinyFolder_Click);
            // 
            // tbSourceFolder
            // 
            this.tbSourceFolder.Location = new System.Drawing.Point(95, 24);
            this.tbSourceFolder.Name = "tbSourceFolder";
            this.tbSourceFolder.ReadOnly = true;
            this.tbSourceFolder.Size = new System.Drawing.Size(410, 20);
            this.tbSourceFolder.TabIndex = 4;
            // 
            // tbDestinyFolder
            // 
            this.tbDestinyFolder.Location = new System.Drawing.Point(95, 56);
            this.tbDestinyFolder.Name = "tbDestinyFolder";
            this.tbDestinyFolder.ReadOnly = true;
            this.tbDestinyFolder.Size = new System.Drawing.Size(410, 20);
            this.tbDestinyFolder.TabIndex = 5;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLog.ForeColor = System.Drawing.Color.ForestGreen;
            this.tbLog.Location = new System.Drawing.Point(16, 335);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(642, 272);
            this.tbLog.TabIndex = 6;
            this.tbLog.TabStop = false;
            this.tbLog.Enter += new System.EventHandler(this.tbLog_Enter);
            // 
            // lbPhotos
            // 
            this.lbPhotos.AutoSize = true;
            this.lbPhotos.Location = new System.Drawing.Point(681, 106);
            this.lbPhotos.Name = "lbPhotos";
            this.lbPhotos.Size = new System.Drawing.Size(43, 13);
            this.lbPhotos.TabIndex = 7;
            this.lbPhotos.Text = "Photos:";
            // 
            // lbVideos
            // 
            this.lbVideos.AutoSize = true;
            this.lbVideos.Location = new System.Drawing.Point(681, 136);
            this.lbVideos.Name = "lbVideos";
            this.lbVideos.Size = new System.Drawing.Size(42, 13);
            this.lbVideos.TabIndex = 8;
            this.lbVideos.Text = "Videos:";
            // 
            // lbPhotosCounter
            // 
            this.lbPhotosCounter.AutoSize = true;
            this.lbPhotosCounter.Location = new System.Drawing.Point(730, 106);
            this.lbPhotosCounter.Name = "lbPhotosCounter";
            this.lbPhotosCounter.Size = new System.Drawing.Size(51, 13);
            this.lbPhotosCounter.TabIndex = 9;
            this.lbPhotosCounter.Text = "0/ 10000";
            // 
            // lbVideosCounter
            // 
            this.lbVideosCounter.AutoSize = true;
            this.lbVideosCounter.Location = new System.Drawing.Point(730, 136);
            this.lbVideosCounter.Name = "lbVideosCounter";
            this.lbVideosCounter.Size = new System.Drawing.Size(51, 13);
            this.lbVideosCounter.TabIndex = 10;
            this.lbVideosCounter.Text = "0/ 10000";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(684, 162);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(414, 40);
            this.btnCopy.TabIndex = 11;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // copyProgressBar
            // 
            this.copyProgressBar.Location = new System.Drawing.Point(684, 206);
            this.copyProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.copyProgressBar.Name = "copyProgressBar";
            this.copyProgressBar.Size = new System.Drawing.Size(414, 20);
            this.copyProgressBar.Step = 1;
            this.copyProgressBar.TabIndex = 12;
            // 
            // tbLogErrors
            // 
            this.tbLogErrors.ForeColor = System.Drawing.Color.Red;
            this.tbLogErrors.Location = new System.Drawing.Point(16, 99);
            this.tbLogErrors.Multiline = true;
            this.tbLogErrors.Name = "tbLogErrors";
            this.tbLogErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogErrors.Size = new System.Drawing.Size(642, 230);
            this.tbLogErrors.TabIndex = 13;
            this.tbLogErrors.TabStop = false;
            this.tbLogErrors.Enter += new System.EventHandler(this.tbLogErrors_Enter);
            // 
            // btnVerifyIntegrity
            // 
            this.btnVerifyIntegrity.Location = new System.Drawing.Point(684, 247);
            this.btnVerifyIntegrity.Margin = new System.Windows.Forms.Padding(2);
            this.btnVerifyIntegrity.Name = "btnVerifyIntegrity";
            this.btnVerifyIntegrity.Size = new System.Drawing.Size(414, 40);
            this.btnVerifyIntegrity.TabIndex = 14;
            this.btnVerifyIntegrity.Text = "Verify Integrity";
            this.btnVerifyIntegrity.UseVisualStyleBackColor = true;
            this.btnVerifyIntegrity.Click += new System.EventHandler(this.btnVerifyIntegrity_Click);
            // 
            // tbLogIntegrity
            // 
            this.tbLogIntegrity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogIntegrity.Location = new System.Drawing.Point(684, 335);
            this.tbLogIntegrity.Margin = new System.Windows.Forms.Padding(2);
            this.tbLogIntegrity.Multiline = true;
            this.tbLogIntegrity.Name = "tbLogIntegrity";
            this.tbLogIntegrity.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogIntegrity.Size = new System.Drawing.Size(414, 272);
            this.tbLogIntegrity.TabIndex = 15;
            this.tbLogIntegrity.Enter += new System.EventHandler(this.tbLogIntegrity_Enter);
            // 
            // integrityProgressBar
            // 
            this.integrityProgressBar.Location = new System.Drawing.Point(684, 291);
            this.integrityProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.integrityProgressBar.Name = "integrityProgressBar";
            this.integrityProgressBar.Size = new System.Drawing.Size(414, 20);
            this.integrityProgressBar.Step = 1;
            this.integrityProgressBar.TabIndex = 16;
            // 
            // btnPauseCopy
            // 
            this.btnPauseCopy.Location = new System.Drawing.Point(972, 129);
            this.btnPauseCopy.Name = "btnPauseCopy";
            this.btnPauseCopy.Size = new System.Drawing.Size(126, 26);
            this.btnPauseCopy.TabIndex = 17;
            this.btnPauseCopy.Text = "Pause";
            this.btnPauseCopy.UseVisualStyleBackColor = true;
            this.btnPauseCopy.Click += new System.EventHandler(this.btnPauseCopy_Click);
            // 
            // btnResumeCopy
            // 
            this.btnResumeCopy.Location = new System.Drawing.Point(972, 99);
            this.btnResumeCopy.Name = "btnResumeCopy";
            this.btnResumeCopy.Size = new System.Drawing.Size(126, 26);
            this.btnResumeCopy.TabIndex = 18;
            this.btnResumeCopy.Text = "Resume";
            this.btnResumeCopy.UseVisualStyleBackColor = true;
            this.btnResumeCopy.Click += new System.EventHandler(this.btnResumeCopy_Click);
            // 
            // checkBoxIncludeYear
            // 
            this.checkBoxIncludeYear.AutoSize = true;
            this.checkBoxIncludeYear.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIncludeYear.Location = new System.Drawing.Point(684, 58);
            this.checkBoxIncludeYear.Name = "checkBoxIncludeYear";
            this.checkBoxIncludeYear.Size = new System.Drawing.Size(89, 17);
            this.checkBoxIncludeYear.TabIndex = 21;
            this.checkBoxIncludeYear.Text = "Include Year:";
            this.checkBoxIncludeYear.UseVisualStyleBackColor = true;
            this.checkBoxIncludeYear.CheckedChanged += new System.EventHandler(this.checkBoxIncludeYear_CheckedChanged);
            // 
            // checkBoxIncludeMonth
            // 
            this.checkBoxIncludeMonth.AutoSize = true;
            this.checkBoxIncludeMonth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxIncludeMonth.Location = new System.Drawing.Point(788, 59);
            this.checkBoxIncludeMonth.Name = "checkBoxIncludeMonth";
            this.checkBoxIncludeMonth.Size = new System.Drawing.Size(97, 17);
            this.checkBoxIncludeMonth.TabIndex = 22;
            this.checkBoxIncludeMonth.Text = "Include Month:";
            this.checkBoxIncludeMonth.UseVisualStyleBackColor = true;
            this.checkBoxIncludeMonth.CheckedChanged += new System.EventHandler(this.checkBoxIncludeMonth_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 647);
            this.Controls.Add(this.checkBoxIncludeMonth);
            this.Controls.Add(this.checkBoxIncludeYear);
            this.Controls.Add(this.btnResumeCopy);
            this.Controls.Add(this.btnPauseCopy);
            this.Controls.Add(this.integrityProgressBar);
            this.Controls.Add(this.tbLogIntegrity);
            this.Controls.Add(this.btnVerifyIntegrity);
            this.Controls.Add(this.tbLogErrors);
            this.Controls.Add(this.copyProgressBar);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lbVideosCounter);
            this.Controls.Add(this.lbPhotosCounter);
            this.Controls.Add(this.lbVideos);
            this.Controls.Add(this.lbPhotos);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.tbDestinyFolder);
            this.Controls.Add(this.tbSourceFolder);
            this.Controls.Add(this.btnChooseDestinyFolder);
            this.Controls.Add(this.btnChooseSourceFolder);
            this.Controls.Add(this.lblDestinyFolder);
            this.Controls.Add(this.lblSourceFolder);
            this.Name = "Form1";
            this.Text = "BackupPhotoVideos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSourceFolder;
        private System.Windows.Forms.Label lblDestinyFolder;
        private System.Windows.Forms.Button btnChooseSourceFolder;
        private System.Windows.Forms.Button btnChooseDestinyFolder;
        private System.Windows.Forms.TextBox tbSourceFolder;
        private System.Windows.Forms.TextBox tbDestinyFolder;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lbPhotos;
        private System.Windows.Forms.Label lbVideos;
        private System.Windows.Forms.Label lbPhotosCounter;
        private System.Windows.Forms.Label lbVideosCounter;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ProgressBar copyProgressBar;
        private System.Windows.Forms.TextBox tbLogErrors;
        private System.Windows.Forms.Button btnVerifyIntegrity;
        private System.Windows.Forms.TextBox tbLogIntegrity;
        private System.Windows.Forms.ProgressBar integrityProgressBar;
        private System.Windows.Forms.Button btnPauseCopy;
        private System.Windows.Forms.Button btnResumeCopy;
        private System.Windows.Forms.CheckBox checkBoxIncludeYear;
        private System.Windows.Forms.CheckBox checkBoxIncludeMonth;
    }
}

