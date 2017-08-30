using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareLab.Sys;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media.Imaging;

namespace SoftwareLab.Components.YouTube
{
    public class Video : ObservableObject
    {
        #region private props
        string _linkUrl;
        string _embedUrl;
        string _thumbNailUrl;
        #endregion

        #region public propertie
        public string LinkUrl
        {
            get { return _linkUrl; }
            internal set
            {
                _linkUrl = value;
                RaisePropertyChanged("LinkUrl");
            }
        }

        public string EmbedUrl
        {
            get { return _embedUrl; }
            internal set
            {
                _embedUrl = value;
                RaisePropertyChanged("EmbedUrl");
            }
        }

        public string ThumbNailUrl
        {
            get { return _thumbNailUrl; }
            internal set
            {
                _thumbNailUrl = value;
                RaisePropertyChanged("ThumbNailUrl");
                
            }
        }

        
        #endregion

        #region ctor
        public Video() 
        { }

        public Video(string linkUrl, string embedUrl, string thumbNailUrl)
        {
            LinkUrl = linkUrl;
            EmbedUrl = embedUrl;
            ThumbNailUrl = thumbNailUrl;
        }
        #endregion



        public override string ToString()
        {
            return string.Format("LinkUrl: {0}; EmbedUrl: {1}; ThumbNailUrl: {2}", _linkUrl, _embedUrl, _thumbNailUrl);
        }
    }



    

    

    
}
