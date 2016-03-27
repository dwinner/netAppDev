using System.Web.Profile;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Класс оболочки над профилем пользователя
   /// </summary>   
   public class UserProfileWrapper
   {
      private readonly ProfileBase _profile;
      private bool _hasChanged;

      /// <summary>
      /// Имя
      /// </summary>
      public string FirstName
      {
         get
         {
            return (string)_profile["FirstName"];
         }
         set
         {
            if (FirstName == value)
            {
               return;
            }

            _profile["FirstName"] = value;
            _hasChanged = true;
         }
      }

      /// <summary>
      /// Отчество
      /// </summary>
      public string PatronymicName
      {
         get
         {
            return (string)_profile["PatronymicName"];
         }
         set
         {
            if (PatronymicName == value)
            {
               return;
            }

            _profile["PatronymicName"] = value;
            _hasChanged = true;
         }
      }

      /// <summary>
      /// Фамилия
      /// </summary>
      public string LastName
      {
         get
         {
            return (string)_profile["LastName"];
         }
         set
         {
            if (LastName == value)
            {
               return;
            }

            _profile["LastName"] = value;
            _hasChanged = true;
         }
      }

      /// <summary>
      /// Количество сущностей на одной странице
      /// </summary>
      public int PageSize
      {
         get
         {
            return (int)_profile["PageSize"];
         }
         set
         {
            if (PageSize == value)
            {
               return;
            }

            _profile["PageSize"] = value;
            _hasChanged = true;
         }
      }

      /// <summary>
      /// Максимальный размер изображения
      /// </summary>
      public int MaxPictureMeasurement
      {
         get
         {
            return (int)_profile["MaxPictureMeasurement"];
         }
         set
         {
            if (MaxPictureMeasurement == value)
            {
               return;
            }

            _profile["MaxPictureMeasurement"] = value;
            _hasChanged = true;
         }
      }

      /// <summary>
      /// Конструктор оболочки профиля пользователя
      /// </summary>
      /// <param name="profile">Объект доступа к текущему профилю</param>
      public UserProfileWrapper(ProfileBase profile)
      {
         _profile = profile;
         FirstName = (string)_profile["FirstName"];
         PatronymicName = (string)_profile["PatronymicName"];
         LastName = (string)_profile["LastName"];
         PageSize = (int)_profile["PageSize"];
         MaxPictureMeasurement = (int)_profile["MaxPictureMeasurement"];
      }

      public UserProfileWrapper()
      {
         _profile = new ProfileBase();
      }

      /// <summary>
      /// Сохранение профиля
      /// </summary>
      public void Save()
      {
         if (_hasChanged)
         {
            _profile.Save();
         }
      }
   }
}