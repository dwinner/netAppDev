﻿using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRChat
{
   /// <summary>
   /// WinForms host for a SignalR server. The host can stop and start the SignalR
   /// server, report errors when trying to start the server on a URI where a
   /// server is already being hosted, and monitor when clients connect and disconnect. 
   /// The hub used in this server is a simple echo service, and has the same 
   /// functionality as the other hubs in the SignalR Getting Started tutorials.
   /// </summary>
   public partial class WinFormsServer : Form
   {
      private IDisposable SignalR { get; set; }
      private const string ServerUri = "http://localhost:8080";

      internal WinFormsServer()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Calls the StartServer method with Task.Run to not
      /// block the UI thread. 
      /// </summary>
      private void ButtonStart_Click(object sender, EventArgs e)
      {
         WriteToConsole("Starting server...");
         ButtonStart.Enabled = false;
         Task.Run(() => StartServer());
      }

      /// <summary>
      /// Stops the server and closes the form. Restart functionality omitted
      /// for clarity.
      /// </summary>
      private void ButtonStop_Click(object sender, EventArgs e)
      {
         //SignalR will be disposed in the FormClosing event
         Close();
      }

      /// <summary>
      /// Starts the server and checks for error thrown when another server is already 
      /// running. This method is called asynchronously from Button_Start.
      /// </summary>
      private void StartServer()
      {
         try
         {
            SignalR = WebApp.Start(ServerUri);
         }
         catch (TargetInvocationException)
         {
            WriteToConsole("Server failed to start. A server is already running on " + ServerUri);
            //Re-enable button to let user try to start server again
            Invoke((Action)(() => ButtonStart.Enabled = true));
            return;
         }
         Invoke((Action)(() => ButtonStop.Enabled = true));
         WriteToConsole("Server started at " + ServerUri);
      }
      /// <summary>
      /// This method adds a line to the RichTextBoxConsole control, using Invoke if used
      /// from a SignalR hub thread rather than the UI thread.
      /// </summary>
      /// <param name="message"></param>
      internal void WriteToConsole(String message)
      {
         if (RichTextBoxConsole.InvokeRequired)
         {
            Invoke((Action)(() =>
                WriteToConsole(message)
            ));
            return;
         }
         RichTextBoxConsole.AppendText(message + Environment.NewLine);
      }

      private void WinFormsServer_FormClosing(object sender, FormClosingEventArgs e)
      {
         SignalR?.Dispose();
      }
   }

   class Startup
   {
      public void Configuration(IAppBuilder app)
      {
         app.UseCors(CorsOptions.AllowAll);
         app.MapSignalR();
      }
   }

   public class MyHub : Hub
   {
      public void Send(string name, string message)
      {
         Clients.All.addMessage(name, message);
      }
      public override Task OnConnected()
      {
         Program.MainForm.WriteToConsole("Client connected: " + Context.ConnectionId);
         return base.OnConnected();
      }

      public override Task OnDisconnected(bool stopCalled)
      {
         Program.MainForm.WriteToConsole($"Client disconnected: {Context.ConnectionId}");
         return base.OnDisconnected(stopCalled);
      }
   }
}
