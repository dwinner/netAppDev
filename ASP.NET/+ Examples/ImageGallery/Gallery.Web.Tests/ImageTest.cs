using System.Linq;
using System.Web.Mvc;
using Gallery.DataLevel;
using Gallery.DataLevel.Orm;
using Gallery.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Gallery.Web.Tests
{
   /// <summary>
   /// Тестирование работоспособности логики, отвечающей за загрузку изображений на страницы
   /// </summary>
   [TestFixture]
   public class ImageTest
   {
      [Test]
      public void ValidImageDataTest()
      {
         var picture = new PictureGallery
         {
            PictureId = 2,
            PictureDescription = "Your description",
            PictureData = new byte[] { },
            PictureMimeType = "image/png"
         };

         var mock = new Mock<IPictureGalleryRepository>();
         mock.Setup(repository => repository.Pictures).Returns(new[]
         {
            new PictureGallery {PictureId = 1, PictureDescription = "Description 1"},
            picture,
            new PictureGallery {PictureId = 3, PictureDescription = "Description 3"}
         }.AsQueryable());

         var controller = new PictureController(mock.Object);

         ActionResult result = controller.GetImage(2);

         Assert.IsNotNull(result);
         Assert.IsInstanceOf<FileResult>(result);
         Assert.AreEqual(picture.PictureMimeType, ((FileResult)result).ContentType);
      }

      [Test]
      public void InvalidImageDataTest()
      {
         var mock = new Mock<IPictureGalleryRepository>();
         mock.Setup(repository => repository.Pictures).Returns(new[]
         {
            new PictureGallery {PictureId = 1, PictureDescription = "Description 1"},
            new PictureGallery {PictureId = 3, PictureDescription = "Description 3"}
         }.AsQueryable());

         var controller = new PictureController(mock.Object);
         var contentResult = controller.GetImage(100);

         Assert.IsNull(contentResult);
      }
   }
}
