using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using Gallery.DataLevel;
using Gallery.DataLevel.Orm;
using Gallery.Web.BusinessLogic;
using Gallery.Web.Filters;
using Gallery.Web.Models;
using Gallery.Web.Properties;
using JpegMetadata.Library;
using FileIo = System.IO.File;

namespace Gallery.Web.Controllers
{
   /// <summary>
   /// Контроллер действий с изображениями
   /// </summary>
   [Authorize]
   [PictureLocalStorage]
   public class PictureController : Controller
   {
      private readonly IPictureGalleryRepository _pictureGalleryRepository;
      private string _profileUserName;

      /// <summary>
      /// Событие при редактировании изображения
      /// </summary>
      public event EventHandler<EditPictureEventArgs> ImageEdited;

      /// <summary>
      /// Данные инициализированы
      /// </summary>
      public bool DataObtained { get; set; }

      /// <summary>
      /// Кол-во элементов на странице
      /// </summary>
      public int PageSize { get; set; }

      /// <summary>
      /// Id-вошедшего пользователя
      /// </summary>
      public int AccountId { get; set; }

      /// <summary>
      /// Безопасный вызов события управления изображениями
      /// </summary>
      /// <param name="e">Аргументы события управления изображениями</param>
      protected virtual void OnImageEdited(EditPictureEventArgs e)
      {
         EventHandler<EditPictureEventArgs> handler = ImageEdited;
         if (handler != null)
         {
            handler(this, e);
         }
      }

      /// <summary>
      /// Конструктор контроллера действий с изображениями
      /// </summary>
      /// <param name="pictureGalleryRepository">Интерфейс для операций управления изображениями</param>
      public PictureController(IPictureGalleryRepository pictureGalleryRepository)
      {
         _pictureGalleryRepository = pictureGalleryRepository;

         ImageEdited += (sender, args) =>
         {
            if (Request == null) return;
            string localStorage = string.Format("{0}\\App_Data\\{1}", Request.PhysicalApplicationPath, args.UserName);
            if (!Directory.Exists(localStorage)) return;

            var picture = args.Picture;
            string imageFileName = picture.PictureId.ToString(CultureInfo.InvariantCulture);
            string mimeTypeSuffix = string.Format("_{0}", picture.PictureMimeType.Replace('/', '_'));
            string fileExtension = Path.GetExtension(picture.PictureFileName);
            var imagePath = string.Format("{0}\\{1}{2}{3}", localStorage, imageFileName, mimeTypeSuffix, fileExtension);

            switch (args.EditState)
            {
               case EditState.Insert:
               case EditState.Update:
                  FileIo.WriteAllBytes(imagePath, picture.PictureData);
                  break;

               case EditState.Delete:
                  FileIo.Delete(imagePath);
                  break;

               default:
                  return;
            }
         };
      }

      /// <summary>
      /// Список изображений
      /// </summary>
      /// <param name="page">Номер страницы</param>
      /// <returns>Результат действия</returns>
      public ViewResult List(int page = 1)
      {
         InitializePersonalInformation();

         var pictureListViewModel = new PictureListViewModel
         {
            Pictures =
               _pictureGalleryRepository.Pictures
                  .OrderBy(gallery => gallery.PictureId)
                  .Where(gallery => gallery.Account.AccountId == AccountId)
                  .Skip((page - 1) * PageSize)
                  .Take(PageSize),
            PageNavigator = new PagingNavigator
            {
               CurrentPage = page,
               ItemsPerPage = PageSize,
               TotalItems = _pictureGalleryRepository.Pictures.Count(pic => pic.Account.AccountId == AccountId)
            }
         };

         return View(pictureListViewModel);
      }

      /// <summary>
      /// Редактирование изобржений
      /// </summary>
      /// <param name="pictureId">Id-изображения</param>
      /// <returns>Результат действия</returns>
      public ViewResult Edit(int pictureId)
      {
         PictureGallery picture = _pictureGalleryRepository.Pictures.FirstOrDefault(pic => pic.PictureId == pictureId);
         return View(picture);
      }

      /// <summary>
      /// Редактирование изобржений
      /// </summary>
      /// <param name="picture">Изображение</param>
      /// <param name="imageFile">Загруженный файл с изображением</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      public ActionResult Edit(PictureGallery picture, HttpPostedFileBase imageFile)
      {
         InitializePersonalInformation();

         if (!ModelState.IsValid)
         {
            return View(picture);
         }

         EditState editState;
         if (picture.PictureId == 0)
         {
            if (imageFile == null || imageFile.FileName == null)
            {
               TempData["message"] = Resources.NeedToSelectFileToUpload;
               return View(picture);
            }

            editState = EditState.Insert;
         }
         else
         {
            editState = imageFile == null || imageFile.FileName == null
               ? EditState.UpdateDescriptionOnly
               : EditState.Update;
         }

         switch (editState)
         {
            case EditState.Insert:
            case EditState.Update:
               string stateMessage;
               bool successValue = TryInitializePicture(picture, imageFile, out stateMessage);
               if (successValue)
               {
                  _pictureGalleryRepository.Save(picture);
                  TempData["message"] = stateMessage;
               }
               else
               {
                  TempData["message"] = stateMessage;
                  return View(picture);
               }
               break;

            case EditState.UpdateDescriptionOnly:
               _pictureGalleryRepository.Save(picture, true);
               TempData["message"] = string.Format(Resources.PictureChangeDescription, picture.PictureFileName);
               break;
         }

         OnImageEdited(new EditPictureEventArgs(_profileUserName, editState, picture));
         return RedirectToAction("List");
      }

      /// <summary>
      /// Создание изображения
      /// </summary>
      /// <returns>Результат действия</returns>
      public ViewResult Create()
      {
         return View("Edit", new PictureGallery());
      }

      /// <summary>
      /// Удаление изображения
      /// </summary>
      /// <param name="pictureId">Id-изображения</param>
      /// <returns>Результат действия</returns>
      [HttpPost]
      public ActionResult Delete(int pictureId)
      {
         InitializePersonalInformation();

         PictureGallery deletedPicture = _pictureGalleryRepository.Delete(pictureId);
         if (deletedPicture != null)
         {
            TempData["message"] = string.Format(Resources.FileWasDeleted, deletedPicture.PictureFileName);
         }
         OnImageEdited(new EditPictureEventArgs(_profileUserName, EditState.Delete, deletedPicture));

         return RedirectToAction("List");
      }

      /// <summary>
      /// Получение изображения с диска или из БД
      /// </summary>
      /// <param name="pictureId">Id-изображения</param>
      /// <returns>Результат действия</returns>
      public FileContentResult GetImage(int pictureId)
      {
         InitializePersonalInformation();

         if (Request != null)
         {
            string localStorage = string.Format("{0}\\App_Data\\{1}", Request.PhysicalApplicationPath, _profileUserName);
            var pictureFileName =
               Directory.GetFiles(localStorage, string.Format("{0}*.*", pictureId), SearchOption.TopDirectoryOnly)
                  .FirstOrDefault();
            if (pictureFileName != null)
            {
               var coreFileName = Path.GetFileNameWithoutExtension(pictureFileName);
               var splitters = coreFileName.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries);
               string contentType = string.Format("{0}{1}{2}", splitters[1], '/', splitters[2]);

               return File(FileIo.ReadAllBytes(pictureFileName), contentType);
            }
         }

         PictureGallery picture = _pictureGalleryRepository.Pictures.FirstOrDefault(pic => pic.PictureId == pictureId);
         return picture != null ? File(picture.PictureData, picture.PictureMimeType) : null;
      }

      protected override void Dispose(bool disposing)
      {
         if (_pictureGalleryRepository != null)
         {
            _pictureGalleryRepository.Dispose();
         }

         base.Dispose(disposing);
      }

      #region Вспомогательные методы

      /// <summary>
      /// Инициализация необходимой информации из профиля пользователя
      /// </summary>
      private void InitializePersonalInformation()
      {
         if (DataObtained || User == null || string.IsNullOrWhiteSpace(User.Identity.Name))
         {
            return;
         }

         string identityName = User.Identity.Name;
         var profileWrapper = new UserProfileWrapper(ProfileBase.Create(identityName));
         _profileUserName = identityName;
         AccountId = _pictureGalleryRepository.RetrieveAccountId(_profileUserName);
         PageSize = profileWrapper.PageSize;

         DataObtained = true;
      }

      /// <summary>
      /// Попытка инициализации изображения
      /// </summary>
      /// <param name="picture">Изображение</param>
      /// <param name="imageFile">Файл с изображением</param>
      /// <param name="stateMessage">Сообщение о состоянии инициализации</param>
      /// <returns>true, если инициализация прошла успешно, false - в противном случае</returns>
      private bool TryInitializePicture(PictureGallery picture, HttpPostedFileBase imageFile, out string stateMessage)
      {
         if (imageFile == null || imageFile.FileName == null)
         {
            stateMessage = Resources.NeedToSelectFileToUpload;
            return false;
         }

         Stream imageStream = imageFile.InputStream;
         byte[] compressedImageBytes = ImageProcessorUtils.ChangeCompressionQuality(imageStream);
         picture.PictureData = compressedImageBytes;
         int imageWidth, imageHeight;
         ImageProcessorUtils.GetImageDimensions(imageStream, out imageWidth, out imageHeight);
         picture.PictureDescription = picture.PictureDescription ?? string.Empty;
         picture.PictureFileName = imageFile.FileName;
         picture.PictureMimeType = imageFile.ContentType;
         picture.Width = imageWidth;
         picture.Height = imageHeight;
         picture.AccountId = AccountId;
         var pictureDetail = PictureDetailActivities.InitializePictureDetail(picture, imageFile);
         picture.PictureDetail = pictureDetail;
         stateMessage = string.Format(Resources.FileHasSaved, picture.PictureFileName);

         return true;
      }

      #endregion

   }
}
