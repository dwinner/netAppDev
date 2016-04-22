namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель входа для внешних служб
   /// </summary>
   public class ExternalLogin
   {
      /// <summary>
      /// Провайдер
      /// </summary>
      public string Provider { get; set; }

      /// <summary>
      /// Имя для отображения провайдера
      /// </summary>
      public string ProviderDisplayName { get; set; }

      /// <summary>
      /// UserId-провайдера
      /// </summary>
      public string ProviderUserId { get; set; }
   }
}
