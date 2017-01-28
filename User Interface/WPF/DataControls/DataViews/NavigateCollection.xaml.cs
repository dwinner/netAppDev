using System;
using System.Windows;
using System.Windows.Data;

namespace DataBinding
{	
	public partial class NavigateCollection
	{
		private readonly ListCollectionView _view;

		public NavigateCollection()
		{
			InitializeComponent();
			var products = App.StoreDb.GetProducts();
			DataContext = products;
			_view = (ListCollectionView) CollectionViewSource.GetDefaultView(DataContext);
			_view.CurrentChanged += OnCurrentChanged;
			ProductComboBox.ItemsSource = products;
		}

		private void OnNext(object sender, RoutedEventArgs e) => _view.MoveCurrentToNext();

		private void OnPrevious(object sender, RoutedEventArgs e) => _view.MoveCurrentToPrevious();

		private void OnSelectionChanged(object sender, RoutedEventArgs e) => _view.MoveCurrentTo(ProductComboBox.SelectedItem);

		private void OnCurrentChanged(object sender, EventArgs e)
		{
			PositionTextBlock.Text = $"Record {_view.CurrentPosition + 1} of {_view.Count}";
			PrevButton.IsEnabled = _view.CurrentPosition > 0;
			NextButton.IsEnabled = _view.CurrentPosition < _view.Count - 1;
		}
	}
}