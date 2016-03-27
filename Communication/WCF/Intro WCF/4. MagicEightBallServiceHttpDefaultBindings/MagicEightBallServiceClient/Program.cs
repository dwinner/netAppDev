using System;
using MagicEightBallServiceClient.ServiceReference;

namespace MagicEightBallServiceClient
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Ask the Magic 8 Ball *****\n");

         // Используем привязку на основе HTTP
         using (EightBallClient ball = new EightBallClient("BasicHttpBinding_IEightBall"))
         {
            Console.Write("Your question: ");
            string question = Console.ReadLine();
            string answer = ball.ObtainAnswerToQuestion(question);
            Console.WriteLine("HTTP; 8-Ball says: {0}", answer);
         }
         Console.ReadLine();

         // Используем привязку на основе TCP
         using (EightBallClient eightBallClient = new EightBallClient("NetTcpBinding_IEightBall"))
         {
            Console.Write("Your question: ");
            string question = Console.ReadLine();
            string answer = eightBallClient.ObtainAnswerToQuestion(question);
            Console.WriteLine("TCP; 8-Ball says: {0}", answer);
         }

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }
   }
}
