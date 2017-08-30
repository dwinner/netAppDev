using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Internet
{
    public class DownloadResponse : ObservableObject
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }


        private byte[] _content;

        public byte[] Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        private DownloadRequest _request;

        public DownloadRequest Request
        {
            get { return _request; }
            internal set
            {
                _request = value;
                RaisePropertyChanged("Request");
            }
        }

    }
}

