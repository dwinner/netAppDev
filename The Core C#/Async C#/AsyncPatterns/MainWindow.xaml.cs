using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AsyncLib;

namespace AsyncPatterns
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainWindow
   {
      private readonly SearchInfo _searchInfo;

      private readonly object _lockList = new object();

      private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

      public MainWindow()
      {
         InitializeComponent();
         _searchInfo = new SearchInfo();
         DataContext = _searchInfo;
         BindingOperations.EnableCollectionSynchronization(_searchInfo.SearchItemResults, _lockList);
      }

      private IEnumerable<IImageRequest> GetSearchRequests()   // Note: Можно еще добавить Google и/или Yahoo
      {
         yield return new BingRequest { SearchTerm = _searchInfo.SearchTerm };
         yield return new FlickrRequest { SearchTerm = _searchInfo.SearchTerm };
      }

      private void ClearButton_OnClick(object sender, RoutedEventArgs e)
      {
         _searchInfo.SearchItemResults.Clear();
      }

      #region Синхронный запрос

      private void SearchSyncButton_OnClick(object sender, RoutedEventArgs e)
      {
         foreach (var searchRequest in GetSearchRequests())
         {
            var client = new WebClient { Credentials = searchRequest.Credentials };
            string response = client.DownloadString(searchRequest.Url);
            IEnumerable<SearchItemResult> itemResults = searchRequest.Parse(response);
            foreach (var image in itemResults)
            {
               _searchInfo.SearchItemResults.Add(image);
            }
         }
      }

      #endregion

      #region Асинхронный запрос

      private void SearchAsyncPatternButton_OnClick(object sender, RoutedEventArgs e)
      {
         Func<string, ICredentials, string> downloadStringFunc = (address, credentials) =>
         {
            var client = new WebClient { Credentials = credentials };
            return client.DownloadString(address);
         };

         Action<SearchItemResult> addItemAction = item => _searchInfo.SearchItemResults.Add(item);

         foreach (var imageRequest in GetSearchRequests())
         {
            IImageRequest localRequest = imageRequest;
            downloadStringFunc.BeginInvoke(imageRequest.Url, imageRequest.Credentials, asyncResult =>
            {
               string response = downloadStringFunc.EndInvoke(asyncResult);
               IEnumerable<SearchItemResult> images = localRequest.Parse(response);
               foreach (var image in images)
               {
                  Dispatcher.Invoke(addItemAction, image);
               }
            }, null);
         }
      }

      #endregion

      #region Асинхронный запрос, основанный на событиях

      private void SearchAsyncEventPatternButton_OnClick(object sender, RoutedEventArgs e)
      {
         foreach (var request in GetSearchRequests())
         {
            var client = new WebClient { Credentials = request.Credentials };
            IImageRequest localRequest = request;
            client.DownloadStringCompleted += (o, args) =>
            {
               string response = args.Result;
               IEnumerable<SearchItemResult> images = localRequest.Parse(response);
               foreach (var image in images)
               {
                  _searchInfo.SearchItemResults.Add(image);
               }
            };
            client.DownloadStringAsync(new Uri(request.Url));
         }
      }

      #endregion

      #region Простой пример асинхронного запроса, основанного на задачах

      private async void TaskBasedAsyncPatternButton_OnClick(object sender, RoutedEventArgs e)
      {
         foreach (var request in GetSearchRequests())
         {
            var client = new WebClient { Credentials = request.Credentials };
            string response = await client.DownloadStringTaskAsync(request.Url);

            var images = request.Parse(response);
            foreach (var image in images)
            {
               _searchInfo.SearchItemResults.Add(image);
            }
         }
      }

      #endregion

      #region Пример асинхронного запроса, основанного на задачах, с возможностью отмены операции

      private async void AltTaskBased_OnClick(object sender, RoutedEventArgs e)
      {
         _cancellationTokenSource = new CancellationTokenSource();

         try
         {
            foreach (var request in GetSearchRequests())
            {
               var httpClient = new HttpClient(new HttpClientHandler { Credentials = request.Credentials });

               var responseMessage = await httpClient.GetAsync(request.Url, _cancellationTokenSource.Token);
               var response = await responseMessage.Content.ReadAsStringAsync();

               IImageRequest localRequest = request;
               await Task.Run(() =>
               {
                  var images = localRequest.Parse(response);
                  foreach (var image in images)
                  {
                     _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                     _searchInfo.SearchItemResults.Add(image);
                  }
               }, _cancellationTokenSource.Token);
            }
         }
         catch (OperationCanceledException canceledEx)
         {
            MessageBox.Show(canceledEx.Message);
         }
      }

      #endregion

      private void CancelButton_OnClick(object sender, RoutedEventArgs e)
      {
         if (_cancellationTokenSource != null)
         {
            _cancellationTokenSource.Cancel();
         }
      }
   }
}
