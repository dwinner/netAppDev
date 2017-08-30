using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using SoftwareLab.Sys.Collections.ObjectModel;

namespace SoftwareLab.Components.YouTube
{
    /// Uses XLINQ to create a List<see cref="YouTubeInfo">YouTubeInfo</see> using an Rss feed.
    /// 
    /// The following links are useful information regarding how the YouTube API works 
    /// 
    /// Example url
    ///
    /// http://gdata.youtube.com/feeds/api/videos?q=football+-soccer&alt=rss&orderby=published&start-index=11&max-results=10&v=1
    ///
    ///
    /// API Notes
    /// http://code.google.com/apis/youtube/2.0/developers_guide_protocol_api_query_parameters.html
    /// </summary>
    public class VideoDispenser : INotifyVideosDownloadCompleted
    {
        //private static readonly string SEARCH = "http://gdata.youtube.com/feeds/api/videos?q={0}&alt=rss&&max-results=20&v=1";
        private static readonly string SEARCH = "http://gdata.youtube.com/feeds/api/videos?q={0}&orderby=relevance&start-index=1&max-results=40&v=2&alt=rss";

        public void LoadVideosAsync(ThreadSafeObservableCollection<Video> collectionToFill, string keyWord, int results, int stratAt)
        {
            // TODO: Verify CancellationToken http://stackoverflow.com/questions/3712939/cancellation-token-in-task-static readonlyructor-why

            Task.Factory.StartNew(() => LoadVideosAsyncInternal(collectionToFill, Guid.NewGuid(), keyWord, results, stratAt));
        }

        public void LoadVideosAsync(Guid downloadEventId, string keyWord, int results, int stratAt)
        {
            ThreadSafeObservableCollection<Video> collectionToFill = new ThreadSafeObservableCollection<Video>();
            Task.Factory.StartNew(() => LoadVideosAsyncInternal(collectionToFill, downloadEventId, keyWord, results, stratAt));
        }

        private void LoadVideosAsyncInternal(ThreadSafeObservableCollection<Video> collectionToFill, Guid downloadEventId, string keyWord, int results, int stratAt)
        {
            try
            {
                var xraw = XElement.Load(string.Format(SEARCH, keyWord));

                var xroot = XElement.Parse(xraw.ToString());
                var links = from item in xroot.Element("channel").Descendants("item")
                             select new Video(item.Element("link").Value, GetEmbedUrlFromLink(item.Element("link").Value), GetThumbNailUrlFromLink(item));


                foreach (var i in links)
                {
                    collectionToFill.Add(i);
                }

                RaiseDownloadCompleted(collectionToFill, downloadEventId);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
        }

        

        #region Download Event
        public event VideosDownloadCompletedEventHandler DownloadCompleted;

        protected virtual void OnDownloadCompleted(VideosDownloadCompletedEventArgs e)
        {
            var handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaiseDownloadCompleted(ThreadSafeObservableCollection<Video> downloadedVideos, Guid downloadEventId)
        {
            OnDownloadCompleted(new VideosDownloadCompletedEventArgs() { Id = downloadEventId, DownloadedVideos = downloadedVideos });
        }
        #endregion

        //#region Download Event
        //public event DownloadCompletedEventHandler DownloadCompleted;

        //protected virtual void OnDownloadCompleted(DownloadCompletedEventArgs e)
        //{
        //    var handler = DownloadCompleted;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}

        //protected void RaiseDownloadCompleted(ThreadSafeObservableCollectionEx<Video> downloadedVideos, Guid downloadEventId)
        //{
        //    OnDownloadCompleted(new DownloadCompletedEventArgs() { Id = downloadEventId, DownloadedVideos = downloadedVideos });
        //}
        //#endregion

        #region Private Methods

        /// <summary>
        /// Simple helper methods that tunrs a link string into a embed string
        /// for a YouTube item. 
        /// turns 
        /// http://www.youtube.com/watch?v=hV6B7bGZ0_E
        /// into
        /// http://www.youtube.com/embed/hV6B7bGZ0_E
        /// </summary>
        private static string GetEmbedUrlFromLink(string link)
        {
            try
            {
                string embedUrl = link.Substring(0, link.IndexOf("&")).Replace("watch?v=", "embed/");
                return embedUrl;
            }
            catch
            {
                return link;
            }
        }


        private static string GetThumbNailUrlFromLink(XElement element)
        {

            XElement group = null;
            XElement thumbnail = null;
            string thumbnailUrl = @"../Images/logo.png";

            foreach (XElement desc in element.Descendants())
            {
                if (desc.Name.LocalName == "group")
                {
                    group = desc;
                    break;
                }
            }

            if (group != null)
            {
                foreach (XElement desc in group.Descendants())
                {
                    if (desc.Name.LocalName == "thumbnail")
                    {
                        thumbnailUrl = desc.Attribute("url").Value.ToString();
                        break;
                    }
                }
            }

            return thumbnailUrl;
        }

        #endregion

    }
}
