using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Gallery.DataLevel;
using Gallery.DataLevel.Orm;
using Gallery.Web.Controllers;
using Gallery.Web.HtmlHelperExtensions;
using Gallery.Web.Models;
using Moq;
using NUnit.Framework;

namespace Gallery.Web.Tests
{
   /// <summary>
   /// Тестирование работоспособности страничного навигатора
   /// </summary>
   [TestFixture]
   public class PaginationTest
   {
      private IList<string> _imageFileNames;
      private PictureGallery[] _pictures;
      private Account _account;
      private Mock<IPictureGalleryRepository> _repositoryMock;

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
         _repositoryMock = new Mock<IPictureGalleryRepository>();
         _repositoryMock.Setup(repository => repository.Pictures).Returns(_pictures.AsQueryable());
      }

      [Test]
      public void PageNavigationTest()
      {
         const int pageSize = 2;
         var pictureController = new PictureController(_repositoryMock.Object)
         {
            DataObtained = true,
            PageSize = pageSize,
            AccountId = _account.AccountId
         };

         var viewModel = (PictureListViewModel)pictureController.List().ViewData.Model;
         var pictures = viewModel.Pictures.ToArray();
         Assert.IsTrue(pictures.Length == pictureController.PageSize);
         for (int i = 0; i < pictures.Length; i++)
         {
            Assert.AreEqual(pictures[i].PictureDescription, string.Format("Description {0}", i));
         }
      }

      [Test]
      public void GeneratingPageLinksTest()
      {
         var navigator = new PagingNavigator
         {
            CurrentPage = 2,
            ItemsPerPage = 10,
            TotalItems = 28
         };
         MvcHtmlString pageLinks = ((HtmlHelper) null).PageLinks(navigator, pageIndex => string.Format("Page{0}", pageIndex));
         const string expectedHtml = "<a href=\"Page1\">1</a><a class=\"selected\" href=\"Page2\">2</a><a href=\"Page3\">3</a>";
         Assert.AreEqual(expectedHtml, pageLinks.ToString());
      }

      [Test]
      public void CanStoreInViewModelTest()
      {
         const int pageSize = 2;
         var pictureController = new PictureController(_repositoryMock.Object)
         {
            DataObtained = true,
            PageSize = pageSize,
            AccountId = _account.AccountId
         };

         var viewModel = (PictureListViewModel)pictureController.List().ViewData.Model;

         PagingNavigator navigator = viewModel.PageNavigator;
         Assert.AreEqual(1, navigator.CurrentPage);
         Assert.AreEqual(2, navigator.ItemsPerPage);
         Assert.AreEqual(3, navigator.TotalItems);
         Assert.AreEqual(2, navigator.TotalPages);
      }
   }
}
