using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal readonly struct IntegrityInfo
    {
        readonly string _log;
        readonly int _errorCounter;
        readonly FileType _fileType;
        readonly bool _isError;

        public IntegrityInfo(string log,  int errorCounter, FileType fileType, bool isError)
        {
            _log = log;
            _errorCounter = errorCounter;
            _fileType = fileType;
            _isError = isError;
        }

        public string Log { get { return _log; } }
        public int ErrorCounter { get { return _errorCounter; } }
        public FileType FileType { get { return _fileType; } }

        public bool IsError { get { return _isError; } }
    }
}
