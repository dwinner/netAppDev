﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigableXamarinFormsPage.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2016 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Camera.UI
{
   /// <summary>
   ///    Navigable xamarin forms page.
   /// </summary>
   internal interface INavigableXamarinFormsPage
   {
      #region Methods

      /// <summary>
      ///    Called when page is navigated to.
      /// </summary>
      /// <returns>The navigated to.</returns>
      /// <param name="navigationParameters">Navigation parameters.</param>
      void OnNavigatedTo(IDictionary<string, object> navigationParameters);

      #endregion
   }
}