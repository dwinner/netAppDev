using System;

namespace MagicEightBallServiceLib
{
   /// <summary>
   /// Реализация контрактов WCF
   /// </summary>
   public class MagicEightBallService : IEightBall
   {
      private static readonly string[] Answers =
         {
            "Future Uncertain",
            "Yes",
            "No",
            "Hazy",
            "Ask again later",
            "Definitely",
            "Maybe"
         };

      public string ObtainAnswerToQuestion(string userQuestion)
      {
         Random random = new Random();
         return Answers[random.Next(Answers.Length)];
      }
   }
}
