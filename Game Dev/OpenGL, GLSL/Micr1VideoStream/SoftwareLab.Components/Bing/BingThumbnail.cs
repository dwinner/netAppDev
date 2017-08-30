using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Bing
{
    public class BingThumbnail : ObservableObject
    {
        private string _mediaUrl;

        public string MediaUrl
        {
            get { return _mediaUrl; }
            set 
            { 
                _mediaUrl = value;
                RaisePropertyChanged("MediaUrl");
            }
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set 
            { 
                _width = value;
                RaisePropertyChanged("Width");
            }
        }

        private int _height;

        public int Height
        {
            get { return _height; }
            set 
            { 
                _height = value;
                RaisePropertyChanged("Height");
            }
        }

        private double _fileSize;

        public double FileSize
        {
            get { return _fileSize; }
            set 
            { 
                _fileSize = value;
                RaisePropertyChanged("FileSize");
            }
        }

        private string _contentType;

        public string ContentType
        {
            get { return _contentType; }
            set 
            {
                _contentType = value;
                RaisePropertyChanged("ContentType");
            }
        }
    }
}
