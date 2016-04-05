using System;
using System.Web.Mvc;
using System.Web.Routing;
using Gallery.DataLevel;
using Ninject;

namespace Gallery.Web.Ioc
{
   /// <summary>
   /// Фабрика контроллеров для контейнера внедрения зависимостей
   /// </summary>
   public class ControllerFactoryImpl : DefaultControllerFactory
   {
      private readonly IKernel _kernel;

      /// <summary>
      /// Инициализатор фабрики контроллеров
      /// </summary>
      public ControllerFactoryImpl()
      {
         _kernel = new StandardKernel();
         AddBindings();
      }

      /// <summary>
      /// Экземпляр контроллера для заданного контекста запроса и типа контроллера
      /// </summary>
      /// <param name="requestContext">Контекст HTTP-запроса, включающий в себя контекст HTTP и данные маршрута</param>
      /// <param name="controllerType">Тип контроллера.</param>
      /// <returns>Экземпляр контроллера</returns>
      protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
      {
         return controllerType == null ? null : _kernel.Get(controllerType) as IController;
      }

      /// <summary>
      /// Добавление инвертирующих связей
      /// </summary>
      private void AddBindings()
      {
         _kernel.Bind<IPictureGalleryRepository>().To<PictureGalleryRepositoryImpl>();
      }
   }
}