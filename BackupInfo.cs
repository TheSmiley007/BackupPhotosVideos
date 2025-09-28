using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal readonly struct BackupInfo
    {
        readonly int _counter;
        readonly FileType _fileType;
        readonly string _log;
        readonly bool _isError;
        readonly int _errorCounter;

        public BackupInfo(int counter, FileType fileType, string log, bool isError, int errorCounter)
        {
            _counter = counter;
            _fileType = fileType;
            _log = log;
            _isError = isError;
            _errorCounter = errorCounter;
        }

        public int Counter { get { return _counter; } }
        public FileType FileType { get { return _fileType; } }
        public string Log { get { return _log; } }
        public bool IsError { get { return _isError; } }

        public int ErrorCounter { get { return _errorCounter; } }

    }
}
