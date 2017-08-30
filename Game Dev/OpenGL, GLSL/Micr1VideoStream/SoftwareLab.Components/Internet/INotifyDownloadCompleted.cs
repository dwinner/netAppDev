using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareLab.Components.Internet
{
    public interface INotifyDownloadCompleted
    {
        // Events
        event DownloadCompletedEventHandler DownloadCompleted;
        // event DownloadCompletedEventHandler DownloadCompleted;
    }
}
