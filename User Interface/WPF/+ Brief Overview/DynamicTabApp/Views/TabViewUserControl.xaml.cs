using System;
using System.Windows;
using DynamicTab.Lib.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicTabApp.Views
{
	public partial class TabViewUserControl
	{
		public TabViewUserControl()
		{
			InitializeComponent();
			var app = Application.Current as App;
			if (app == null)
				throw new InvalidOperationException("app cannot be null");

			ViewModel = app.Container.GetService<TabViewModel>();
			DataContext = this;
		}

		public TabViewModel ViewModel { get; private set; }
	}
}