using System.Collections.ObjectModel;

namespace AsyncLib
{
   /// <summary>
   /// Информация о поиске
   /// </summary>
   public class SearchInfo : BindableBase
   {
      private string _searchTerm;

      /// <summary>
      /// Ввод, полученный для поиска
      /// </summary>
      public string SearchTerm
      {
         get { return _searchTerm; }
         set { SetProperty(ref _searchTerm, value); }
      }

      public ObservableCollection<SearchItemResult> SearchItemResults { get; private set; }

      public SearchInfo()
      {
         SearchItemResults = new ObservableCollection<SearchItemResult>();
         SearchItemResults.CollectionChanged += (sender, args) => OnPropertyChanged("SearchItemResults");
      }
   }
}