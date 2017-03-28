using System;
using System.Windows;
using DynamicTab.Lib.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicTabApp.Views
{
	public partial class ListViewUserControl
	{
		public ListViewUserControl()
		{
			InitializeComponent();
			var app = Application.Current as App;
			if (app == null)
				throw new InvalidOperationException("app cannot be null");

			ViewModel = app.Container.GetService<ListViewModel>();
			DataContext = this;
		}

		public ListViewModel ViewModel { get; private set; }
	}
}