using Gallery.DataLevel.Orm;
using Gallery.Web.Annotations;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Аргументы события при редактировании изображения
   /// </summary>
   public class EditPictureEventArgs
   {
      [NotNull]
      private readonly string _userName;

      private readonly EditState _editState;

      [NotNull]
      private readonly PictureGallery _picture;

      /// <summary>
      /// Имя пользователя при входе
      /// </summary>
      [NotNull]
      public string UserName
      {
         get { return _userName; }
      }

      /// <summary>
      /// Изображение
      /// </summary>
      [NotNull]
      public PictureGallery Picture
      {
         get { return _picture; }
      }

      /// <summary>
      /// Состояние редактирования
      /// </summary>
      public EditState EditState
      {
         get { return _editState; }
      }

      /// <summary>
      /// Конструктор аргументов события при редактировании изображения
      /// </summary>
      /// <param name="userName">Имя пользователя при входе</param>
      /// <param name="editState">Состояние редактирования</param>
      /// <param name="picture">Изображение</param>
      public EditPictureEventArgs(string userName, EditState editState, PictureGallery picture)
      {
         _userName = userName;
         _editState = editState;
         _picture = picture;
      }
   }
}