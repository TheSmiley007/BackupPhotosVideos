using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal class Unsubscriber<BackupInfo> : IDisposable
    {
        private readonly ISet<IObserver<BackupInfo>> _observers;
        private readonly IObserver<BackupInfo> _observer;

        public Unsubscriber(ISet<IObserver<BackupInfo>> observers, IObserver<BackupInfo> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }
}
