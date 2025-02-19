﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModule.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Autofac;

namespace Camera.Portable.Ioc
{
   /// <summary>
   ///    Module.
   /// </summary>
   public interface IModule
   {
      #region Methods

      /// <summary>
      ///    Register the specified builder.
      /// </summary>
      /// <param name="builder">builder.</param>
      void Register(ContainerBuilder builder);

      #endregion
   }
}