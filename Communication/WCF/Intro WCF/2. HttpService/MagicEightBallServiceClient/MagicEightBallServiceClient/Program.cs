using System;
using System.ServiceModel;
using MagicEightBallServiceClient.ServiceReference;   // Нахождение прокси

namespace MagicEightBallServiceClient
{
   public static class Program
   {
      static void Main()
      {
         Console.WriteLine("----- Ask the magic 8 ball -----");
         using (EightBallClient eightBallClient = new EightBallClient())
         {
            Console.Write("Your question: ");
            string question = Console.ReadLine();
            try
            {
               string answer = eightBallClient.ObtainAnswerToQuestion(question);
               Console.WriteLine("8-ball says: {0}", answer);
            }
            catch (EndpointNotFoundException endpointNotFoundEx)
            {
               Console.WriteLine(endpointNotFoundEx.Message);
            }
         }
         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }
   }
}
