using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BackupPhotosVideos
{
    public partial class Form1 : Form, IObserver<BackupInfo>, IObserver<IntegrityInfo>
    {
        private BackupManager backupManager;
        private IDisposable unsubscribeBackup;
        private int backupNumErrors;
        private bool hasBackedUpFirstPhoto;
        private bool hasBackedUpFirstVideo;

        private IntegrityManager integrityManager;
        private IDisposable unsubscribeIntegrity;
        private int integrityNumErrors;
        private bool hasVerifiedFirstPhoto;
        private bool hasVerifiedFirstVideo;


        private Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            backupManager = new BackupManager();
            unsubscribeBackup = backupManager.Subscribe(this);

            integrityManager = new IntegrityManager();
            unsubscribeIntegrity = integrityManager.Subscribe(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideCounters();
            HideCopy();
            HideIntegrity();
            SetPauseResumeBtnsToDefaultState();

            checkBoxIncludeYear.Checked = PathMaker.IncludeYear;
            checkBoxIncludeMonth.Checked = PathMaker.IncludeMonth;
        }

        private void HideCopy()
        {
            btnCopy.Hide();
            copyProgressBar.Hide();
        }
        private void ShowCopy()
        {
            btnCopy.Show();
        }
        private void TryShowCopy()
        {
            if (!string.IsNullOrEmpty(backupManager.DestinationFolder) &&
                !string.IsNullOrEmpty(backupManager.SourceFolder) &&
                backupManager.TotalFiles != 0)
            {
                ShowCopy();
            }
        }

        private void HideIntegrity()
        {
            btnVerifyIntegrity.Hide();
            integrityProgressBar.Hide();
            tbLogIntegrity.Hide();
        }
        private void ShowIntegrity()
        {
            if (btnVerifyIntegrity.InvokeRequired)
            {
                btnVerifyIntegrity.Invoke(new MethodInvoker(delegate
                {
                    btnVerifyIntegrity.Show();
                }));
            }
            else
            {
                btnVerifyIntegrity.Show();
            }

            if (tbLogIntegrity.InvokeRequired)
            {
                tbLogIntegrity.Invoke(new MethodInvoker(delegate
                {
                    tbLogIntegrity.Show();
                }));
            }
            else
            {
                tbLogIntegrity.Show();
            }

        }

        private void HideCounters()
        {
            lbVideos.Hide();
            lbVideosCounter.Hide();
            lbPhotos.Hide();
            lbPhotosCounter.Hide();
        }
        private void ShowCounters()
        {
            lbVideos.Show();
            lbVideosCounter.Show();
            lbPhotos.Show();
            lbPhotosCounter.Show();
        }
        private void SetPhotosCounter(int currPhoto)
        {
            string percentage = (backupManager.NumPhotos != 0) ? (Math.Round(((double)currPhoto / backupManager.NumPhotos) * 100, 1).ToString()) + "%" : "No photos";
            if (lbPhotosCounter.InvokeRequired)
            {
                lbPhotosCounter.Invoke(new MethodInvoker(delegate
                {
                    lbPhotosCounter.Text = $"{currPhoto}/{backupManager.NumPhotos} ({percentage})";
                }));
            }
            else
            {
                lbPhotosCounter.Text = $"{currPhoto}/{backupManager.NumPhotos} ({percentage})";
            }

        }
        private void SetVideosCounter(int currVideo)
        {
            string percentage = (backupManager.NumVideos != 0) ? (Math.Round(((double)currVideo / backupManager.NumVideos) * 100, 1).ToString()) + "%" : "No videos";
            if (lbVideosCounter.InvokeRequired)
            {
                lbVideosCounter.Invoke(new MethodInvoker(delegate
                {
                    lbVideosCounter.Text = $"{currVideo}/{backupManager.NumVideos} ({percentage})";
                }));
            }
            else
            {
                lbVideosCounter.Text = $"{currVideo}/{backupManager.NumVideos} ({percentage})";
            }
            
        }

        private void HidePauseResumeBtns()
        {
            if (btnPauseCopy.InvokeRequired)
            {
                btnPauseCopy.Invoke(new MethodInvoker(delegate { btnPauseCopy.Hide(); }));
            }
            else
            {
                btnPauseCopy.Hide();
            }

            if (btnResumeCopy.InvokeRequired)
            {
                btnResumeCopy.Invoke(new MethodInvoker(delegate { btnResumeCopy.Hide(); }));
            }
            else
            {
                btnResumeCopy.Hide();
            }
        }
        private void ShowPauseResumeBtns()
        {
            btnPauseCopy.Show();
            btnResumeCopy.Show();
        }
        private void SetPauseResumeBtnsToDefaultState()
        {
            btnResumeCopy.Enabled = false;
            btnPauseCopy.Enabled = true;
            HidePauseResumeBtns();
        }

        private void btnChooseSourceFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Source Folder";
            DialogResult result = fd.ShowDialog();

            if (result == DialogResult.OK)
            {
                backupManager.SourceFolder = fd.SelectedPath;
                tbSourceFolder.Text = fd.SelectedPath;

                ShowCounters();
                TryShowCopy();

                copyProgressBar.Maximum = backupManager.TotalFiles;
                integrityProgressBar.Maximum = backupManager.TotalFiles;
            }
            else if (string.IsNullOrEmpty(backupManager.SourceFolder))
            {
                HideCounters();
                HideCopy();
            }

            SetPhotosCounter(0);
            SetVideosCounter(0);
        }
        private void btnChooseDestinyFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Destiny Folder";
            DialogResult result = fd.ShowDialog();

            if (result == DialogResult.OK)
            {
                backupManager.DestinationFolder = fd.SelectedPath;
                tbDestinyFolder.Text = fd.SelectedPath;

                TryShowCopy();
            }
            else if (string.IsNullOrEmpty(backupManager.DestinationFolder))
            {
                HideCopy();
            }
        }

        #region Backup_Process
        private void btnCopy_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
            tbLogErrors.Clear();
            copyProgressBar.Value = 0;
            copyProgressBar.Show();

            SetPhotosCounter(0);
            SetVideosCounter(0);

            btnCopy.Enabled = false;

            hasBackedUpFirstPhoto = false;
            hasBackedUpFirstVideo = false;

            stopwatch.Restart();
            Thread backupThread = new Thread(new ThreadStart(() => { backupManager.Backup(); }));
            backupThread.IsBackground = true;
            backupThread.Start();

            ShowPauseResumeBtns();
        }

        void IObserver<BackupInfo>.OnNext(BackupInfo value)
        {
            if (value.FileType == FileType.Photo)
            {
                if (!hasBackedUpFirstPhoto)
                {
                    hasBackedUpFirstPhoto = true;

                    tbLog.Invoke(new MethodInvoker(delegate { tbLog.AppendText("--- PHOTOS ---\r\n"); }));
                    tbLogErrors.Invoke(new MethodInvoker(delegate { tbLogErrors.AppendText("--- PHOTOS ---\r\n"); }));
                }

                SetPhotosCounter(value.Counter);
            }
            else if (value.FileType == FileType.Video)
            {
                if (!hasBackedUpFirstVideo)
                {
                    hasBackedUpFirstVideo = true;
                    tbLog.Invoke(new MethodInvoker(delegate { tbLog.AppendText("\r\n--- VIDEOS ---\r\n"); }));
                    tbLogErrors.Invoke(new MethodInvoker(delegate { tbLogErrors.AppendText("\r\n--- VIDEOS ---\r\n"); }));
                }

                SetVideosCounter(value.Counter);
            }

            copyProgressBar.Invoke(new MethodInvoker(delegate { copyProgressBar.PerformStep(); }));

            backupNumErrors = value.ErrorCounter;
            if (value.IsError)
            {
                tbLogErrors.Invoke(new MethodInvoker(delegate { tbLogErrors.AppendText(value.Log); }));
                return;
            }

            tbLog.Invoke(new MethodInvoker(delegate { tbLog.AppendText(value.Log); }));
        }

        void IObserver<BackupInfo>.OnError(Exception error)
        {
        }

        void IObserver<BackupInfo>.OnCompleted()
        {
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            MessageBox.Show($"Backup done, verify if all the photos and videos were copied!\r\n" +
                $"Completed in: {ts.Days}d {ts.Hours}h:{ts.Minutes}m:{ts.Seconds}s.{ts.Milliseconds}ms\r\n" +
                $"Errors: {backupNumErrors}", "Backup Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            tbLogErrors.Invoke(new MethodInvoker(delegate
            {
                tbLogErrors.SelectionStart = 0;
                tbLogErrors.ScrollToCaret();
            }));

            tbLog.Invoke(new MethodInvoker(delegate
            {
                tbLog.SelectionStart = 0;
                tbLog.ScrollToCaret();
            }));

            copyProgressBar.Invoke(new MethodInvoker(delegate
            {
                copyProgressBar.Hide();
            }));

            SetPauseResumeBtnsToDefaultState();
            btnCopy.Invoke(new MethodInvoker(delegate
            {
                btnCopy.Enabled = true;
            }));

            integrityManager.SourceFiles = backupManager.SourceFiles;
            integrityManager.TargetFiles = backupManager.BackupFiles;
            integrityManager.NumPhotos = backupManager.NumPhotos;
            ShowIntegrity();

        }

        private void btnPauseCopy_Click(object sender, EventArgs e)
        {
            btnPauseCopy.Enabled = false;
            backupManager.MRE.Reset();
            btnResumeCopy.Enabled = true;
        }

        private void btnResumeCopy_Click(object sender, EventArgs e)
        {
            btnResumeCopy.Enabled = false;
            backupManager.MRE.Set();
            btnPauseCopy.Enabled = true;
        }
        #endregion

        #region Integrity_Process

        private void btnVerifyIntegrity_Click(object sender, EventArgs e)
        {
            tbLogIntegrity.Clear();
            integrityProgressBar.Value = 0;
            integrityProgressBar.Show();

            btnVerifyIntegrity.Enabled = false;

            hasVerifiedFirstPhoto = false;
            hasVerifiedFirstVideo = false;

            stopwatch.Restart();
            Thread integrityThread = new Thread(new ThreadStart(() => { integrityManager.Verify(); }));
            integrityThread.IsBackground = true;
            integrityThread.Start();
        }

        void IObserver<IntegrityInfo>.OnNext(IntegrityInfo value)
        {
            if (value.FileType == FileType.Photo)
            {
                if (!hasVerifiedFirstPhoto)
                {
                    hasVerifiedFirstPhoto = true;

                    tbLogIntegrity.Invoke(new MethodInvoker(delegate { tbLogIntegrity.AppendText("--- PHOTOS ---\r\n"); }));
                }
            }
            else if (value.FileType == FileType.Video)
            {
                if (!hasVerifiedFirstVideo)
                {
                    hasVerifiedFirstVideo = true;
                    tbLogIntegrity.Invoke(new MethodInvoker(delegate { tbLogIntegrity.AppendText("\r\n--- VIDEOS ---\r\n"); }));
                }
            }

            integrityProgressBar.Invoke(new MethodInvoker(delegate { integrityProgressBar.PerformStep(); }));

            integrityNumErrors = value.ErrorCounter;
            if (value.IsError)
            {
                tbLogIntegrity.Invoke(new MethodInvoker(delegate { tbLogIntegrity.AppendText(value.Log); }));
            }
        }

        void IObserver<IntegrityInfo>.OnError(Exception error)
        {
        }

        void IObserver<IntegrityInfo>.OnCompleted()
        {
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            MessageBox.Show($"Integrity Verified!\r\n" +
                $"Completed in: {ts.Days}d {ts.Hours}h:{ts.Minutes}m:{ts.Seconds}s.{ts.Milliseconds}ms\r\n" +
                $"Errors: {integrityNumErrors}", "Integrity Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            tbLogIntegrity.Invoke(new MethodInvoker(delegate
            {
                tbLogIntegrity.SelectionStart = 0;
                tbLog.ScrollToCaret();
            }));

            integrityProgressBar.Invoke(new MethodInvoker(delegate
            {
                integrityProgressBar.Hide();

            }));

            btnVerifyIntegrity.Invoke(new MethodInvoker(delegate
            {
                btnVerifyIntegrity.Enabled = true;
            }));

        }
        #endregion

        private void FocusToDefault()
        {
            lblSourceFolder.Focus();
        }
        private void tbLogErrors_Enter(object sender, EventArgs e)
        {
            FocusToDefault();
        }
        private void tbLog_Enter(object sender, EventArgs e)
        {
            FocusToDefault();
        }
        private void tbLogIntegrity_Enter(object sender, EventArgs e)
        {
            FocusToDefault();
        }

        private void checkBoxIncludeYear_CheckedChanged(object sender, EventArgs e)
        {
            PathMaker.IncludeYear = checkBoxIncludeYear.Checked;
        }

        private void checkBoxIncludeMonth_CheckedChanged(object sender, EventArgs e)
        {
            PathMaker.IncludeMonth = checkBoxIncludeMonth.Checked;
        }
    }
}
