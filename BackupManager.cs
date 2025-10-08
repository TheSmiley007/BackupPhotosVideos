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
                int startIndexFullName = photo.LastIndexOf('\\') + 1;
                int startIndexExtension = photo.LastIndexOf('.') + 1;
                string fullName = photo.Substring(startIndexFullName);
                string extension = photo.Substring(startIndexExtension);
                string fileName = photo.Substring(startIndexFullName, fullName.Length - extension.Length - 1);

                string log;
                bool isError = false;
                string folderPath = PathMaker.Make(photo, FileType.Photo);
                string destinyPath = null;
                try
                {
                    if (folderPath is null)
                    {
                        throw new Exception("Could not make the destination path!");
                    }

                    destinyPath = Path.Combine(_destinationFolder, folderPath);
                    if (!Directory.Exists(destinyPath))
                    {
                        Directory.CreateDirectory(destinyPath);
                    }
                    destinyPath = Path.Combine(destinyPath, fullName);

                    if (!File.Exists(destinyPath))
                    {
                        File.Copy(photo, destinyPath);
                        log = $"{fullName} -> Copied successfully!\r\n";
                    }
                    else
                    {
                        string copyDestinyPath = Path.Combine(_destinationFolder, folderPath, "Copias");
                        string originalPhotoCopyPath = Path.Combine(copyDestinyPath, fullName);

                        if (!Directory.Exists(copyDestinyPath))
                        {
                            Directory.CreateDirectory(copyDestinyPath);
                        }

                        if (!File.Exists(originalPhotoCopyPath))
                        {
                            File.Copy(photo, originalPhotoCopyPath);
                        }

                        string newFullName = $"{fileName}_{Guid.NewGuid()}.{extension}";
                        destinyPath = Path.Combine(copyDestinyPath, newFullName);
                        File.Copy(photo, destinyPath);

                        log = $"{fullName} -> Copied successfully to {copyDestinyPath} folder!\r\n";
                    }
                }
                catch (Exception ex)
                {
                    log = $"[ERROR] {fullName} -> {ex.Message}\r\n";
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

                int startIndexFullName = video.LastIndexOf('\\') + 1;
                int startIndexExtension = video.LastIndexOf('.') + 1;
                string fullName = video.Substring(startIndexFullName);
                string extension = video.Substring(startIndexExtension);
                string fileName = video.Substring(startIndexFullName, fullName.Length - extension.Length - 1);

                string log;
                bool isError = false;
                string folderPath = PathMaker.Make(video, FileType.Video);
                string destinyPath = null;
                try
                {
                    if (folderPath is null)
                    {
                        throw new Exception("Could not make the destination path!");
                    }

                    destinyPath = Path.Combine(_destinationFolder, folderPath);
                    if (!Directory.Exists(destinyPath))
                    {
                        Directory.CreateDirectory(destinyPath);
                    }
                    destinyPath = Path.Combine(destinyPath, fullName);

                    if (!File.Exists(destinyPath))
                    {
                        File.Copy(video, destinyPath);
                        log = $"{fullName} -> Copied successfully!\r\n";
                    }
                    else
                    {
                        string copyDestinyPath = Path.Combine(_destinationFolder, folderPath, "Copias");
                        string originalPhotoCopyPath = Path.Combine(copyDestinyPath, fullName);

                        if (!Directory.Exists(copyDestinyPath))
                        {
                            Directory.CreateDirectory(copyDestinyPath);
                        }

                        if (!File.Exists(originalPhotoCopyPath))
                        {
                            File.Copy(video, originalPhotoCopyPath);
                        }

                        string newFullName = $"{fileName}_{Guid.NewGuid()}.{extension}";
                        destinyPath = Path.Combine(copyDestinyPath, newFullName);
                        File.Copy(video, destinyPath);

                        log = $"{fullName} -> Copied successfully to {copyDestinyPath} folder!\r\n";
                    }
                }
                catch (Exception ex)
                {
                    log = $"[ERROR] {fullName} -> {ex.Message}\r\n";
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
