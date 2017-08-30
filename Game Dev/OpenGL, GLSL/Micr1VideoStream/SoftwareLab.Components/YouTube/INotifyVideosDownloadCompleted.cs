using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareLab.Components.YouTube
{
    public interface INotifyVideosDownloadCompleted
    {
        // Events
        event VideosDownloadCompletedEventHandler DownloadCompleted;
        // event DownloadCompletedEventHandler DownloadCompleted;
    }
}
