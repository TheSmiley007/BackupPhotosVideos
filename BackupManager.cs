using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal class BackupManager : IObservable<BackupInfo>
    {
        private readonly HashSet<IObserver<BackupInfo>> _observers = new HashSet<IObserver<BackupInfo>>();
        private string _sourceFolder;
        private string _destinationFolder;

        private string[] _sourceFiles;
        private string[] _photoFiles;
        private string[] _videoFiles;

        private string[] _backupFiles;

        private int _numPhotos;
        private int _numVideos;
        private int _totalFiles;

        private ManualResetEvent mre = new ManualResetEvent(true);

        public ManualResetEvent MRE { get { return mre; } }

        public string SourceFolder
        {
            get
            {
                return _sourceFolder;
            }
            set
            {
                _sourceFolder = value;


                IEnumerable<string> files = Directory.EnumerateFiles(_sourceFolder, "*.*", SearchOption.AllDirectories);
                _photoFiles = files
                        .Where(file => BackupRules.AllowedPhotosExtensions.Any(file.ToLower().EndsWith))
                        .ToArray();

                _videoFiles = files
                        .Where(file => BackupRules.AllowedVideosExtensions.Any(file.ToLower().EndsWith))
                        .ToArray();

                _numPhotos = _photoFiles.Length;
                _numVideos = _videoFiles.Length;

                _sourceFiles = new string[_photoFiles.Length + _videoFiles.Length];
                _photoFiles.CopyTo(_sourceFiles, 0);
                _videoFiles.CopyTo(_sourceFiles, _photoFiles.Length);
                _totalFiles = _sourceFiles.Length;


                _backupFiles = new string[_sourceFiles.Length];
            }
        }
        public string DestinationFolder { get { return _destinationFolder; } set { _destinationFolder = value; } }

        public int NumPhotos { get { return _numPhotos; } }
        public int NumVideos { get { return _numVideos; } }
        public int TotalFiles { get { return _totalFiles; } }

        public string[] SourceFiles { get { return _sourceFiles; } }
        public string[] BackupFiles { get { return _backupFiles; } }

        public void Backup()
        {
            if (_photoFiles.Length == 0 && _videoFiles.Length == 0) { return; }
            
            int currFile = 0;
            int numErrors = 0;
            int currPhoto = 0;
            foreach (string photo in _photoFiles)
            {
                int startIndexFName = photo.LastIndexOf('\\') + 1;
                string fName = photo.Substring(startIndexFName);

                string log;
                bool isError = false;
                string destinyPath = null;
                try
                {
                    string sourcePath = photo;
                    destinyPath = PathMaker.Make(sourcePath, FileType.Photo);
                    if (destinyPath is null)
                    {
                        throw new Exception("Could not make the destination path!");
                    }

                    destinyPath = Path.Combine(_destinationFolder, destinyPath);
                    if (!Directory.Exists(destinyPath))
                    {
                        Directory.CreateDirectory(destinyPath);
                    }
                    destinyPath = Path.Combine(destinyPath, fName);

                    File.Copy(sourcePath, destinyPath);
                    
                    log = $"{fName} -> Copied successfully!\r\n";
                }
                catch (Exception ex)
                {
                    log = $"[ERROR] {fName} -> {ex.Message}\r\n";
                    numErrors++;
                    isError = true;
                }
                currPhoto++;

                _backupFiles[currFile] = destinyPath;
                currFile++;

                mre.WaitOne();
                BackupInfo backupInfo = new BackupInfo(currPhoto, FileType.Photo, log, isError, numErrors);
                foreach (IObserver<BackupInfo> observer in _observers)
                {
                    observer.OnNext(backupInfo);
                }
            }

            int currVideo = 0;
            foreach (string video in _videoFiles)
            {

                int startIndexFName = video.LastIndexOf('\\') + 1;
                string fName = video.Substring(startIndexFName);

                string log;
                bool isError = false;
                string destinyPath = null;
                try
                {
                    string sourcePath = video;

                    destinyPath = PathMaker.Make(sourcePath, FileType.Video);
                    if (destinyPath is null)
                    {
                        throw new Exception("Could not make the destination path!");
                    }

                    destinyPath = Path.Combine(_destinationFolder, destinyPath);
                    if (!Directory.Exists(destinyPath))
                    {
                        Directory.CreateDirectory(destinyPath);
                    }
                    destinyPath = Path.Combine(destinyPath, fName);

                    File.Copy(sourcePath, destinyPath);
                    

                    log = $"{fName} -> Copied successfully!\r\n";
                }
                catch (Exception ex)
                {
                    log = $"[ERROR] {fName} -> {ex.Message}\r\n";
                    isError = true;
                    numErrors++;
                }
                currVideo++;

                _backupFiles[currFile] = destinyPath;
                currFile++;

                mre.WaitOne();
                BackupInfo backupInfo = new BackupInfo(currVideo, FileType.Video, log, isError, numErrors);
                foreach (IObserver<BackupInfo> observer in _observers)
                {
                    observer.OnNext(backupInfo);
                }
            }

            foreach (IObserver<BackupInfo> observer in _observers)
            {
                observer.OnCompleted();
            }
             
        }


        public IDisposable Subscribe(IObserver<BackupInfo> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<BackupInfo>(_observers, observer);
        }

    }
}
