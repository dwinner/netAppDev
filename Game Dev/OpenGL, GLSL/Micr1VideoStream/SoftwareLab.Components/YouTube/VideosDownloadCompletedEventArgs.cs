using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;
using SoftwareLab.Sys.Collections.ObjectModel;

namespace SoftwareLab.Components.YouTube
{
    public class VideosDownloadCompletedEventArgs : EventArgs
    {
        // Fields
        private Guid _id;
        private ThreadSafeObservableCollection<Video> _downloadedVideos;

        public VideosDownloadCompletedEventArgs()
        {
            _id = Guid.NewGuid();
        }

        public VideosDownloadCompletedEventArgs(Guid id)
        {
            _id = id;
        }

        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            internal set
            {
                _id = value;
            }
        }

        public virtual ThreadSafeObservableCollection<Video> DownloadedVideos
        {
            get
            {
                return _downloadedVideos;
            }
            internal set
            {
                _downloadedVideos = value;
            }
        }
    }

    
}
