namespace AsyncLib
{
   /// <summary>
   /// Одиночный элемент поиска
   /// </summary>
   public class SearchItemResult : BindableBase
   {
      private string _title;

      private string _url;

      private string _thumbnailUrl;

      private string _source;

      /// <summary>
      /// Текст, описывающий изображение
      /// </summary>
      public string Title
      {
         get { return _title; }
         set { SetProperty(ref _title, value); }
      }

      /// <summary>
      /// Ссылка на изображение крупного размера
      /// </summary>
      public string Url
      {
         get { return _url; }
         set { SetProperty(ref _url, value); }
      }

      /// <summary>
      /// Ссылка на изображение-миниатюру
      /// </summary>
      public string ThumbnailUrl
      {
         get { return _thumbnailUrl; }
         set { SetProperty(ref _thumbnailUrl, value); }
      }

      /// <summary>
      /// Источник
      /// </summary>
      public string Source
      {
         get { return _source; }
         set { SetProperty(ref _source, value); }
      }
   }
}