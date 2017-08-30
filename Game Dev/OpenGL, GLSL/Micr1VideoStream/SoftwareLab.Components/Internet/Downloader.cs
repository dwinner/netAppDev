using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.IO;
using SoftwareLab.Sys.IO.Stream;

namespace SoftwareLab.Components.Internet
{
    public class Downloader : INotifyDownloadCompleted
    {

        public void DownloadContentAsync(DownloadRequest request)
        {
            // TODO: Verify CancellationToken http://stackoverflow.com/questions/3712939/cancellation-token-in-task-static readonlyructor-why

            Task.Factory.StartNew(() => DownloadContentAsyncInternal(request, Guid.NewGuid()));
        }

        public void DownloadContentAsync(DownloadRequest request, Guid downloadEventId)
        {
            Task.Factory.StartNew(() => DownloadContentAsyncInternal(request, downloadEventId));
        }

        private void DownloadContentAsyncInternal(DownloadRequest request, Guid downloadEventId)
        {
            DownloadResponse response = new DownloadResponse(){Content = null, Id = request.Id, Request = request};
            try
            {
                if (string.IsNullOrEmpty(request.SourceUrl) == false)
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(request.SourceUrl);

                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                    {
                        response.Content = StreamConverter.ReadToEnd(httpResponse.GetResponseStream());   
                    }

                    RaiseDownloadCompleted(response, downloadEventId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message, "ERROR");
            }
        }

        #region from stream to byte
        
        #endregion

        #region Download Event
        public event DownloadCompletedEventHandler DownloadCompleted;

        protected virtual void OnDownloadCompleted(DownloadCompletedEventArgs e)
        {
            var handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaiseDownloadCompleted(DownloadResponse response, Guid downloadEventId)
        {
            OnDownloadCompleted(new DownloadCompletedEventArgs() { Id = downloadEventId, DownloadedContent = response });
        }
        #endregion
    }
}
