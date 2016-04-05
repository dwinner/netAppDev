using System.ServiceModel;

namespace MagicEightBallServiceLib
{   
   [ServiceContract(Namespace = "http://denny_home.com")]
   public interface IEightBall
   {
      /// <summary>
      /// Вопрос к волшебному шару
      /// </summary>
      /// <param name="userQuestion">Вопрос к волшебному шару</param>
      /// <returns>Ответ на вопрос</returns>
      [OperationContract]      
      string ObtainAnswerToQuestion(string userQuestion);
   }
}
