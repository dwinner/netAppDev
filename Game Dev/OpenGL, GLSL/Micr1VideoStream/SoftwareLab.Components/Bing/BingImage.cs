using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;

namespace SoftwareLab.Components.Bing
{
    public class BingImage : ObservableObject
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

        private string _title;

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

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

        private string _mediaFilePath;

        public string MediaFilePath
        {
            get { return _mediaFilePath; }
            set 
            {
                _mediaFilePath = value;
                RaisePropertyChanged("MediaFilePath");
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

        private string _displayUrl;

        public string DisplayUrl
        {
            get { return _displayUrl; }
            set 
            { 
                _displayUrl = value;
                RaisePropertyChanged("DisplayUrl");
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


        private BingThumbnail _thub;

        public BingThumbnail Thumb
        {
            get { return _thub; }
            set 
            { 
                _thub = value;
                _thub.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_thub_PropertyChanged);
            }
        }

        void _thub_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Thumb");
        }
    }
}
