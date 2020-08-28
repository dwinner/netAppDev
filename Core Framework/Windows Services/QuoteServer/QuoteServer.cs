/**
 * Код сервера
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuoteServer
{
   public class QuoteServer
   {
      private const string DefaultQuotesFileName = "quotes.txt";
      private const int DefaultListenerPort = 7890;

      private TcpListener _listener;
      private readonly int _port;
      private readonly string _fileName;
      private List<string> _quotes;
      private Random _random;

      public QuoteServer(string fileName, int port)
      {
         //Contract.Requires<ArgumentNullException>(fileName != null);
         //Contract.Requires<ArgumentException>(port >= IPEndPoint.MinPort && port <= IPEndPoint.MaxPort);
         _fileName = fileName;
         _port = port;
      }

      public QuoteServer(string fileName)
         : this(fileName, DefaultListenerPort)
      {
      }

      public QuoteServer()
         : this(DefaultQuotesFileName)
      {
      }

      protected void ReadQuotes()
      {
         try
         {
            _quotes = File.ReadAllLines(_fileName).ToList();
            if (_quotes.Count == 0)
            {
               throw new QuoteException("quotes file is empty");
            }
            _random = new Random();
         }
         catch (IOException ioEx)
         {
            throw new QuoteException("I/O Error", ioEx);
         }
      }

      protected string GetRandomQuoteOfTheDay()
      {
         int index = _random.Next(0, _quotes.Count);
         return _quotes[index];
      }

      public void Start()
      {
         ReadQuotes();
         Task.Factory.StartNew(Listener, TaskCreationOptions.LongRunning);
      }

      private void Listener()
      {
         try
         {
            IPAddress ipAddress = IPAddress.Any;
            _listener = new TcpListener(ipAddress, _port);
            _listener.Start();
            while (true)
            {
               Socket clientSocket = _listener.AcceptSocket();
               string message = GetRandomQuoteOfTheDay();
               var encoder = new UnicodeEncoding();
               byte[] buffer = encoder.GetBytes(message);
               clientSocket.Send(buffer, buffer.Length, 0);
               clientSocket.Close();
            }
         }
         catch (SocketException socketEx)
         {
            Trace.TraceError(string.Format("QuoteServer {0}", socketEx.Message));
            throw new QuoteException("Socket Error", socketEx);
         }
      }      

      public void Stop()
      {
         _listener.Stop();
      }

      public void Suspend()
      {
         _listener.Stop();
      }

      public void Resume()
      {
         Start();           
      }      

      public void RefreshQuotes()
      {
         ReadQuotes();
      }
   }
}