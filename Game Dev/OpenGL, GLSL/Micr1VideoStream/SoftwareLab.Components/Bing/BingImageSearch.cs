using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.ServiceModel.Syndication;
using System.Net.Security;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using System.Configuration;

namespace SoftwareLab.Components.Bing
{
    public class BingImageSearch : INotifyBingImageDownloadCompleted
    {
        private static readonly string USER_ID = ConfigurationManager.AppSettings["UserId"];
        private static readonly string SECURE_ACCOUNT_ID = ConfigurationManager.AppSettings["SecureAccountId"]; 
        
        private static readonly string SERVICE_URI = "https://api.datamarket.azure.com/Data.ashx/Bing/Search/Image?Query=%27{0}%27&Market=%27en-US%27&Adult=%27Strict%27&ImageFilters=%27Aspect%3aWide%27&$top=50&$format=Atom";
        private static readonly string METADATA = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly string DATA_SERVICES = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly string BING_IMAGES = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DownloadedBingImages");

        static BingImageSearch()
        {
            Task.Factory.StartNew(() => DeleteFiles());
        }

        private static void DeleteFiles()
        {
            try
            {
                //System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BING_IMAGES));
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(BING_IMAGES);
                
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }

            }
            catch { }
        }


        public void DownloadImagesAsync(BingImage image, string keyWord)
        {
            // TODO: Verify CancellationToken http://stackoverflow.com/questions/3712939/cancellation-token-in-task-static readonlyructor-why
            Task.Factory.StartNew(() => DownloadImagesAsyncInternal(image, Guid.NewGuid(), keyWord));
        }

        public void DownloadImagesAsync(Guid downloadEventId, string keyWord)
        {  
            BingImage image = new BingImage();
            Task.Factory.StartNew(() => DownloadImagesAsyncInternal(image, downloadEventId, keyWord));
        }

        private void DownloadImagesAsyncInternal(BingImage imageToFill, Guid downloadEventId, string keyWord)
        {
            SyndicationFeed feed;
            List<BingImage> imageList = new List<BingImage>();

            try
            {
                #region webrequest to get bing image results
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = " ",
                    OmitXmlDeclaration = true,
                    Encoding = new UTF8Encoding(false),
                };

                // Create a new HttpWebRequest object for the specified resource.
                // "$-_.+!*'(),"
                // .Replace(" ", "%20")
                WebRequest request = (WebRequest)WebRequest.Create(string.Format(SERVICE_URI, HttpUtility.UrlEncode( keyWord
                    .Replace(@"""", "").Replace("$", "").Replace("-", "").Replace("+", "").Replace("!", "").Replace("*", "")
                    .Replace("'", "").Replace("(", "").Replace(")", "").Replace(",", ""))
                    ));
                
                // Request mutual authentication.
                request.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
                // Supply client credentials.
                request.Credentials = new NetworkCredential(USER_ID, SECURE_ACCOUNT_ID);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var xmlReader = XmlReader.Create(response.GetResponseStream()))
                    {
                        feed = SyndicationFeed.Load(xmlReader);
                    }
                    response.Close();
                }
                #endregion

                #region feed creation

                if (feed != null)
                {
                    foreach (var item in feed.Items)
                    {
                        using (var tempStream = new MemoryStream())
                        {
                            using (var tempWriter = XmlWriter.Create(tempStream, settings))
                            {
                                try
                                {
                                    BingImage image = new BingImage();

                                    item.Content.WriteTo(tempWriter, "Content", "");
                                    tempWriter.Flush();

                                    // Get the content as XML. 
                                    var contentXml = Encoding.UTF8.GetString(tempStream.ToArray());
                                    var contentDocument = XDocument.Parse(contentXml);

                                    // Find the properties element. 
                                    var propertiesName = XName.Get("properties", METADATA);
                                    var propertiesElement = contentDocument.Descendants(propertiesName).FirstOrDefault();

                                    // guid
                                    var ID = XName.Get("ID", DATA_SERVICES);
                                    var IDElement = propertiesElement.Descendants(ID).FirstOrDefault();
                                    image.Id = new Guid(IDElement.Value);

                                    // string
                                    var title = XName.Get("Title", DATA_SERVICES);
                                    var titleElement = propertiesElement.Descendants(title).FirstOrDefault();
                                    image.Title = titleElement.Value;

                                    // string
                                    var mediaUrl = XName.Get("MediaUrl", DATA_SERVICES);
                                    var mediaUrlElement = propertiesElement.Descendants(mediaUrl).FirstOrDefault();
                                    image.MediaUrl = mediaUrlElement.Value;

                                    // string
                                    var sourceUrl = XName.Get("SourceUrl", DATA_SERVICES);
                                    var sourceUrlElement = propertiesElement.Descendants(sourceUrl).FirstOrDefault();
                                    image.SourceUrl = sourceUrlElement.Value;

                                    // string
                                    var displayUrl = XName.Get("DisplayUrl", DATA_SERVICES);
                                    var displayUrlElement = propertiesElement.Descendants(displayUrl).FirstOrDefault();
                                    image.DisplayUrl = displayUrlElement.Value;

                                    // int32
                                    var width = XName.Get("Width", DATA_SERVICES);
                                    var widthElement = propertiesElement.Descendants(width).FirstOrDefault();
                                    int w = 0;
                                    Int32.TryParse(widthElement.Value, out w);
                                    image.Width = w;

                                    // int32
                                    var height = XName.Get("Height", DATA_SERVICES);
                                    var heightElement = propertiesElement.Descendants(height).FirstOrDefault();
                                    int h = 0;
                                    Int32.TryParse(heightElement.Value, out h);
                                    image.Height = h;

                                    // int64
                                    var fileSize = XName.Get("FileSize", DATA_SERVICES);
                                    var fileSizeElement = propertiesElement.Descendants(fileSize).FirstOrDefault();
                                    double f = 0;
                                    double.TryParse(fileSizeElement.Value, out f);
                                    image.FileSize = f;

                                    // string
                                    var contentType = XName.Get("ContentType", DATA_SERVICES);
                                    var contentTypeElement = propertiesElement.Descendants(contentType).FirstOrDefault();
                                    image.ContentType = contentTypeElement.Value;

                                    // Find the properties element. 
                                    var thumbnailName = XName.Get("Thumbnail", DATA_SERVICES);
                                    var thumbnaiElement = contentDocument.Descendants(thumbnailName).FirstOrDefault();
                                    image.Thumb = new BingThumbnail();

                                    // string
                                    var mediaThumbUrl = XName.Get("MediaUrl", DATA_SERVICES);
                                    var mediaThumbUrlElement = propertiesElement.Descendants(mediaThumbUrl).FirstOrDefault();
                                    image.Thumb.MediaUrl = mediaThumbUrlElement.Value;

                                    // string
                                    var contentTypeThumbUrl = XName.Get("ContentType", DATA_SERVICES);
                                    var contentTypeThumbUrlElement = propertiesElement.Descendants(contentTypeThumbUrl).FirstOrDefault();
                                    image.Thumb.ContentType = contentTypeThumbUrlElement.Value;

                                    // int32
                                    var widthThumb = XName.Get("Width", DATA_SERVICES);
                                    var widthThumbElement = propertiesElement.Descendants(widthThumb).FirstOrDefault();
                                    int wt = 0;
                                    Int32.TryParse(widthThumbElement.Value, out wt);
                                    image.Width = wt;

                                    // int32
                                    var heightThumb = XName.Get("Height", DATA_SERVICES);
                                    var heightThumbElement = propertiesElement.Descendants(heightThumb).FirstOrDefault();
                                    int ht = 0;
                                    Int32.TryParse(heightThumbElement.Value, out ht);
                                    image.Height = ht;

                                    // int64
                                    var fileSizeThumb = XName.Get("FileSize", DATA_SERVICES);
                                    var fileSizeThumbElement = propertiesElement.Descendants(fileSizeThumb).FirstOrDefault();
                                    double ft = 0;
                                    double.TryParse(fileSizeThumbElement.Value, out ft);
                                    image.FileSize = ft;

                                    imageList.Add(image);
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine(ex.Message, "ERROR in XmlWriter.Create");
                                }
                            }
                        }
                    }

                }
                #endregion

                #region webrequest for image download
                var imList = imageList.OrderByDescending(c => c.Width);//.ThenByDescending(n => n.Height).ToList();//.FirstOrDefault();

                if (imList != null)
                {
                    // provo a scarica una immagine fichè non ci riesco
                    foreach (BingImage im in imList)
                    {
                        try
                        {
                            if (im != null)
                            {
                                HttpWebRequest requestImage = (HttpWebRequest)HttpWebRequest.Create(im.MediaUrl);

                                byte[] buffer = new byte[4096];
                                string fileName = string.Format("{0}.{1}", Guid.NewGuid(), Path.GetExtension(im.MediaUrl).ToLower());
                                // string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BING_IMAGES);
                                string filePath = BING_IMAGES;
                                string fullFilePath = System.IO.Path.Combine(filePath, fileName);

                                if (Directory.Exists(filePath) == false)
                                    Directory.CreateDirectory(filePath);

                                using (HttpWebResponse responseImage = (HttpWebResponse)requestImage.GetResponse())
                                {
                                    using (var target = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write))
                                    {
                                        using (var stream = responseImage.GetResponseStream())
                                        {
                                            int read;

                                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                target.Write(buffer, 0, read);
                                            }
                                        }
                                    }

                                    //System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
                                    //// Save the response to the output stream
                                    //responseImage.ContentType = "image/gif";
                                    //img.Save(responseImage.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                                }

                                im.MediaFilePath = fullFilePath;


                                imageToFill.Id = im.Id;
                                imageToFill.ContentType = im.ContentType;
                                imageToFill.DisplayUrl = im.DisplayUrl;
                                imageToFill.FileSize = im.FileSize;
                                imageToFill.Height = im.Height;
                                imageToFill.MediaFilePath = im.MediaFilePath;
                                imageToFill.MediaUrl = im.MediaUrl;
                                imageToFill.SourceUrl = im.SourceUrl;
                                if (imageToFill.Thumb == null)
                                    imageToFill.Thumb = new BingThumbnail();
                                imageToFill.Thumb.ContentType = im.Thumb.ContentType;
                                imageToFill.Thumb.FileSize = im.Thumb.FileSize;
                                imageToFill.Thumb.Height = im.Thumb.Height;
                                imageToFill.Thumb.MediaUrl = im.Thumb.MediaUrl;
                                imageToFill.Thumb.Width = im.Thumb.Width;
                                imageToFill.Title = im.Title;
                                imageToFill.Width = im.Width;

                                RaiseDownloadCompleted(imageToFill, downloadEventId);
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.Message, "ERROR in foreach (BingImage");
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR in DownloadImagesAsyncInternal");
            }
        }

        #region Download Event
        public event BingImageDownloadCompletedEventHandler DownloadCompleted;

        protected virtual void OnDownloadCompleted(BingImageDownloadCompletedEventArgs e)
        {
            var handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaiseDownloadCompleted(BingImage downloadedImage, Guid downloadEventId)
        {
            OnDownloadCompleted(new BingImageDownloadCompletedEventArgs() { Id = downloadEventId, DownloadedBingImage = downloadedImage });
        }
        #endregion

    }
}
