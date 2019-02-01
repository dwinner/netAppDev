// Сохранение состояния приложения

using SavingAppState.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SavingAppState
{
   internal static class Program
   {
      private static readonly List<Customer> _Customers = new List<Customer>();
      private static readonly List<Order> _PendingOrders = new List<Order>();
      private static readonly List<Order> _ProcessedOrders = new List<Order>();

      private static void Main()
      {
         var stream = new MemoryStream();
         SaveApplicationState(stream);
         Settings.Default.GlobalState = stream.ToArray();
         Settings.Default.Save();
      }

      private static void SaveApplicationState(Stream stream)
      {
         var formatter = new BinaryFormatter();
         formatter.Serialize(stream, _Customers);
         formatter.Serialize(stream, _PendingOrders);
         formatter.Serialize(stream, _ProcessedOrders);
      }
   }

   [Serializable]
   internal sealed class Customer
   {
   }

   [Serializable]
   internal sealed class Order
   {
   }
}