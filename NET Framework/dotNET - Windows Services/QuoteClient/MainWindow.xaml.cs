using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace QuoteClient
{
   /// <summary>
   /// Оконный клиент сервера цитат
   /// </summary>
   public partial class MainWindow
   {
      private readonly QuoteInformation _quoteInfo = new QuoteInformation();

      public MainWindow()
      {
         InitializeComponent();
         DataContext = _quoteInfo;
      }

      protected async void OnGetQuote(object sender, RoutedEventArgs e)
      {
         const int bufferSize = 1024;
         Cursor currentCursor = Cursor;
         Cursor = Cursors.Wait;
         _quoteInfo.EnableRequest = false;

         string serverName = Properties.Settings.Default.ServerName;
         int portNumber = Properties.Settings.Default.PortNumber;

         var client = new TcpClient();
         NetworkStream stream = null;

         try
         {
            await client.ConnectAsync(serverName, portNumber);
            stream = client.GetStream();
            var buffer = new byte[bufferSize];
            int received = await stream.ReadAsync(buffer, 0, bufferSize);
            if (received <= 0)            
               return;            
            _quoteInfo.Quote = Encoding.Unicode.GetString(buffer).Trim('\0');
         }
         catch (SocketException socketEx)
         {
            MessageBox.Show(socketEx.Message, "Error quote of the day", MessageBoxButton.OK, MessageBoxImage.Error);
         }
         finally
         {
            if (stream != null)
               stream.Close();
            if (client.Connected)
               client.Close();
         }

         Cursor = currentCursor;
         _quoteInfo.EnableRequest = true;
      }
   }
}
