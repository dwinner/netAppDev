using System;
using System.IO;
using System.ServiceProcess;

namespace QuoteService
{
   public partial class QuoteService : ServiceBase
   {
      private const string QuotesFileName = "Quotes.txt";
      private const int DefaultPort = 5678;
      private const int RefreshCommandCode = 128;

      private QuoteServer.QuoteServer _quoteServer;

      public QuoteService()
      {
         InitializeComponent();
      }

      protected override void OnStart(string[] args)  // Запуск службы
      {
         _quoteServer = new QuoteServer.QuoteServer(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, QuotesFileName), DefaultPort);
         _quoteServer.Start();
      }

      protected override void OnStop() // Останов службы
      {
         _quoteServer.Stop();
      }

      protected override void OnPause()   // Приостанов службы
      {
         _quoteServer.Suspend();
      }

      protected override void OnContinue()   // Возобновление работы
      {
         _quoteServer.Resume();
      }

      protected override void OnCustomCommand(int command)  // Обработчик специальных команд (от 128 до 256)
      {
         switch (command)
         {
            case RefreshCommandCode:
               _quoteServer.RefreshQuotes();
               break;
         }
      }
   }
}
