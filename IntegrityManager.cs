using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BackupPhotosVideos
{
    internal class IntegrityManager : IObservable<IntegrityInfo>
    {
        private HashSet<IObserver<IntegrityInfo>> _observers = new HashSet<IObserver<IntegrityInfo>>();
        private string[] _sourceFiles;
        private int _numPhotos;
        private string[] _targetFiles;
        private ManualResetEvent mre = new ManualResetEvent(true);

        public string[] SourceFiles { set { _sourceFiles = value; } }
        public string[] TargetFiles { set { _targetFiles = value; } }
        public int NumPhotos { set { _numPhotos = value; } }

        public ManualResetEvent MRE { get { return mre; } }

        public IDisposable Subscribe(IObserver<IntegrityInfo> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<IntegrityInfo>(_observers, observer);
        }

        public void Verify()
        {
            if (_sourceFiles.Length == 0 || _targetFiles.Length == 0) return;

            int numErrors = 0;

            string log = "";
            for (int i = 0; i < _sourceFiles.Length; i++)
            {
                bool isError = false;

                try
                {
                    if (!File.Exists(_targetFiles[i]))
                    {
                        throw new Exception("File does not exist in folder!");
                    }
                    ReadOnlySpan<byte> sourceHash = ComputeFileHash(_sourceFiles[i]);
                    ReadOnlySpan<byte> destinyHash = ComputeFileHash(_targetFiles[i]);

                    if (!sourceHash.SequenceEqual(destinyHash))
                    {
                        throw new Exception("Corrupted!");
                    }
                }
                catch (Exception ex)
                {
                    log = $"{_targetFiles[i]} -> {ex.Message}\r\n";
                    isError = true;

                    numErrors++;
                }

                mre.WaitOne();
                IntegrityInfo integrityInfo = new IntegrityInfo(log, numErrors, (i + 1 <= _numPhotos) ? FileType.Photo : FileType.Video, isError);
                foreach (IObserver<IntegrityInfo> observer in _observers)
                {
                    observer.OnNext(integrityInfo);
                }
            }

            foreach (IObserver<IntegrityInfo> observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        private byte[] ComputeFileHash(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {

                using (SHA256 sha256 = SHA256.Create())
                {
                    int bytesRead = 0;
                    byte[] buffer = new byte[1024 * 1024];
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        if (bytesRead < buffer.Length)
                        {
                            sha256.TransformFinalBlock(buffer, 0, bytesRead);
                        }
                        else
                        {
                            sha256.TransformBlock(buffer, 0, bytesRead, null, 0);
                        }
                    }

                    return sha256.Hash;
                }
            }
        }
    }
}
