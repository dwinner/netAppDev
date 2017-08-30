using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Internet
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        // Fields
        private Guid _id;
        private DownloadResponse _downloadedContent;

        public DownloadCompletedEventArgs()
        {
            _id = Guid.NewGuid();
        }

        public DownloadCompletedEventArgs(Guid id)
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

        public virtual DownloadResponse DownloadedContent
        {
            get
            {
                return _downloadedContent;
            }
            internal set
            {
                _downloadedContent = value;
            }
        }
    }

    
}
