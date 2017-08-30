using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareLab.Components.Bing
{
    public interface INotifyBingImageDownloadCompleted
    {
        // Events
        event BingImageDownloadCompletedEventHandler DownloadCompleted;
        // event DownloadCompletedEventHandler DownloadCompleted;
    }
}
