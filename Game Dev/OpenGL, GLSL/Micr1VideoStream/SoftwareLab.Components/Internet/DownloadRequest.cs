using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Internet
{
    public class DownloadRequest : ObservableObject
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


        private string _sourceUrl;

        public string SourceUrl
        {
            get { return _sourceUrl; }
            set
            {
                _sourceUrl = value;
                RaisePropertyChanged("SourceUrl");
            }
        }

    }
}
