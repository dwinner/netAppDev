namespace QuizApp
{
   public static class QuizPreferences
   {
      public static string DebugTag = "Trivia Quiz Log";
      // Значения игровых параметров файла предпочтений
      public static string GamePreferences = "GamePrefs";
      public static string GamePreferencesNickname = "Nickname"; // String
      public static string GamePreferencesEmail = "Email"; // String
      public static string GamePreferencesPassword = "Password"; // String
      public static string GamePreferencesDob = "DOB"; // Long

      public static string GamePreferencesGender = "Gender";
         // Integer, in array order: Male (1), Female (2), Undisclosed (0)

      public static string GamePreferencesScore = "Score"; // Integer
      public static string GamePreferencesCurrentQuestion = "CurQuestion"; // Integer
      public static string GamePreferencesAvatar = "Avatar";

      // Константы для имен XML-тэгов
      public static string XmlTagQuestionBlock = "questions";
      public static string XmlTagQuestion = "question";
      public static string XmlTagQuestionAttributeNumber = "number";
      public static string XmlTagQuestionAttributeText = "text";
      public static string XmlTagQuestionAttributeImageurl = "imageUrl";
      public static int QuestionBatchSize = 15;
   }
}