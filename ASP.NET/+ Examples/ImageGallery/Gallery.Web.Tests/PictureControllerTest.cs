using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Gallery.DataLevel;
using Gallery.DataLevel.Orm;
using Gallery.Web.Controllers;
using Gallery.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gallery.Web.Tests
{
   /// <summary>
   /// Тестирование работоспособности контроллера, управляющего страницами с изображениями
   /// </summary>
   [TestFixture]
   public class PictureControllerTest
   {
      private IList<string> _imageFileNames;
      private PictureGallery[] _pictures;
      private Account _account;
      private Mock<IPictureGalleryRepository> _mock;

      [TestFixtureSetUp]
      public void FixtureSetUp()
      {
         _imageFileNames = new List<string>
         {
            "pics/break.jpg",
            "pics/flying steps.jpg",
            "pics/spinning da wheels.jpg"
         };

         _pictures = new PictureGallery[_imageFileNames.Count];
         _account = new Account { AccountId = 1, UserName = "dwinner@inbox.ru" };

         for (int i = 0; i < _imageFileNames.Count; i++)
         {
            _pictures[i] = new PictureGallery
            {
               Account = _account,
               AccountId = _account.AccountId,
               PictureId = i,
               PictureDescription = string.Format("Description {0}", i),
               Width = 2700,
               Height = 1500,
               PictureFileName = _imageFileNames[i],
               PictureData = File.ReadAllBytes(_imageFileNames[i]),
               PictureMimeType = "image/jpeg",
               PictureDetail = null
            };
         }
      }

      [SetUp]
      public void SetUp()
      {
         _mock = new Mock<IPictureGalleryRepository>();
         _mock.Setup(repository => repository.Pictures).Returns(_pictures.AsQueryable());
      }

      [Test]
      public void ActionListTest()
      {
         const int pageSize = 5;
         var pictureController = new PictureController(_mock.Object)
         {
            DataObtained = true,
            PageSize = pageSize,
            AccountId = _account.AccountId
         };

         var viewModel = (PictureListViewModel)pictureController.List().ViewData.Model;
         Assert.AreEqual(viewModel.Pictures.Count(), 3);
         for (int i = 0; i < _pictures.Length; i++)
         {
            Assert.AreEqual(_pictures[i].PictureDescription, viewModel.Pictures.ElementAt(i).PictureDescription);
         }
      }

      [TestCase(0)]
      [TestCase(1)]
      [TestCase(2)]
      public void ActionEditGetTest(int pictureId)
      {
         var pictureController = new PictureController(_mock.Object);
         var currentPicture = pictureController.Edit(pictureId).ViewData.Model as PictureGallery;
         Assert.IsNotNull(currentPicture);
         Assert.AreEqual(pictureId, currentPicture.PictureId);
      }

      [Test]
      public void CannotEditNonexistentPicture()
      {
         var pictureController = new PictureController(_mock.Object);
         var nonExistentPicture = pictureController.Edit(_pictures.Length).ViewData.Model as PictureGallery;
         Assert.IsNull(nonExistentPicture);
      }

      [TestCase(1)]
      [TestCase(2)]
      public void EditDescriptionTest(int editedIndex)
      {
         string newDescription = string.Format("New Description {0}", editedIndex);
         var pictureController = new PictureController(_mock.Object);
         _pictures[editedIndex].PictureDescription = newDescription;
         ActionResult actionResult = pictureController.Edit(_pictures[editedIndex], null);
         _mock.Verify(repository => repository.Save(_pictures[editedIndex], true));
         Assert.IsNotInstanceOf<ViewResult>(actionResult);
         Assert.AreEqual(newDescription, _mock.Object.Pictures.ElementAt(editedIndex).PictureDescription);
      }

      [Test]
      public void ActionDeleteTest()
      {
         var pictureController = new PictureController(_mock.Object);
         int deletedPictureId = _pictures[0].PictureId;
         pictureController.Delete(deletedPictureId);
         _mock.Verify(repository => repository.Delete(deletedPictureId));
      }
   }
}