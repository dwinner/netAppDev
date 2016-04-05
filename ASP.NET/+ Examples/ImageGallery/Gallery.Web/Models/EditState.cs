namespace Gallery.Web.Models
{
   /// <summary>
   /// Состояние редактирования изображения
   /// </summary>
   public enum EditState
   {
      /// <summary>
      /// Вставка новой записи
      /// </summary>
      Insert,

      /// <summary>
      /// Обновление сущетсвующей записи
      /// </summary>
      Update,

      /// <summary>
      /// Обновление описания
      /// </summary>
      UpdateDescriptionOnly,

      /// <summary>
      /// Удаление записи
      /// </summary>
      Delete
   }
}