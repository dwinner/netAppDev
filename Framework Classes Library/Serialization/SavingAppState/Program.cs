// Сохранение состояния приложения

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SavingAppState.Properties;

namespace SavingAppState
{
   internal static class Program
   {
      private static readonly List<Customer> Customers = new List<Customer>();
      private static readonly List<Order> PendingOrders = new List<Order>();
      private static readonly List<Order> ProcessedOrders = new List<Order>();

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
         formatter.Serialize(stream, Customers);
         formatter.Serialize(stream, PendingOrders);
         formatter.Serialize(stream, ProcessedOrders);
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