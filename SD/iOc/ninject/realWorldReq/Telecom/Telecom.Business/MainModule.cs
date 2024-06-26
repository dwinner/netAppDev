﻿using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Telecom.Business
{
   public class MainModule : NinjectModule
   {
      public override void Load()
      {
         Bind<IStatusCollectorFactory>().ToFactory();
         //Bind<IStatusCollector>().To<TcpStatusCollector>().Named("TcpStatusCollector");
         //Bind<IStatusCollector>().To<FileStatusCollector>().Named("FileStatusCollector");
         Bind<IStatusCollector>().To<TcpStatusCollector>()
            .NamedLikeFactoryMethod((IStatusCollectorFactory factory) => factory.GetTcpStatusCollector());
         Bind<IStatusCollector>().To<FileStatusCollector>()
            .NamedLikeFactoryMethod((IStatusCollectorFactory factory) => factory.GetFileStatusCollector());
      }
   }
}