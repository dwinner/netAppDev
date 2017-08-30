using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Bing
{
    public class BingImageDownloadCompletedEventArgs : EventArgs
    {
        // Fields
        private Guid _id;
        private BingImage _downloadedBingImage;

        public BingImageDownloadCompletedEventArgs()
        {
            _id = Guid.NewGuid();
        }

        public BingImageDownloadCompletedEventArgs(Guid id)
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

        public virtual BingImage DownloadedBingImage
        {
            get
            {
                return _downloadedBingImage;
            }
            internal set
            {
                _downloadedBingImage = value;
            }
        }
    }

    
}
