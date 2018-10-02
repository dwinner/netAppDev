using System.Collections;
using System.Windows;
using System.Windows.Data;
using BooksDemo.Data;
using JetBrains.Annotations;

namespace BooksDemo.UserControls
{
   public partial class BookGridUserControl
   {
      private readonly ListCollectionView _view;

      public BookGridUserControl()
      {
         var viewList = new BookFactory().GetBooks() as IList;
         if (viewList != null)
            _view = new ListCollectionView(viewList);

         InitializeComponent();
      }

      [UsedImplicitly]
      public IEnumerable BooksView => _view;

      private void OnGroupChecked(object sender, RoutedEventArgs e)
      {
         if (!_view.CanGroup || _view.GroupDescriptions == null)
            return;

         if (_view.GroupDescriptions.Count == 0)
            _view.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Book.Publisher)));
         else
            _view.GroupDescriptions.Clear();
      }
   }
}