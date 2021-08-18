using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnownTypes.Tests
{
   [TestClass]
   public class MessageProcessorTests
   {
      private IMessageProcessor _channel;
      private ServiceHost _host;

      [TestInitialize]
      public void Init()
      {
         _host = new ServiceHost(typeof(MessageProcessor));
         _host.Open();
         _channel = new ChannelFactory<IMessageProcessor>(string.Empty).CreateChannel();
      }

      [TestCleanup]
      public void CleanUp()
      {
         //(_channel as IDisposable)?.Dispose();
         _host.Close();
      }

      [TestMethod]
      public void ProcessMessage() => Assert.AreEqual("Unknown", _channel.Process(new Message()));

      [TestMethod]
      public void ProcessApplicationClosedMessage()
         => Assert.AreEqual("Application has closed", _channel.Process(new ApplicationClosedMessage("\\SomeMachine")));
   }
}