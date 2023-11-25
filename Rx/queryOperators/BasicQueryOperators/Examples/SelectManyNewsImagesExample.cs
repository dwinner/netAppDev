using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Security.Policy;
using Helpers;

namespace BasicQueryOperators.Examples
{
   public static class SelectManyNewsImagesExample
   {
      public static void Run()
      {
         SimpleCollectionsFlattening();
         ProcessingTheSourceAndTheResult();
         ProcessingTheSourceAndTheResultWithLet();
      }

      private static void ProcessingTheSourceAndTheResultWithLet()
      {
         Demo.DisplayHeader(
            "The SelectMany operator is added automatically when using Let inside the query-syntax query");

         #region creating collection of news items

         var theNews = new[]
         {
            new NewsItem
            {
               Title = "NewsItem1",
               Url = new Url("https://news.com/NewsItem1"),
               Images =
                  new[]
                  {
                     new NewsImage { ImageName = "Item1Image1" },
                     new NewsImage { ImageName = "Item1Image2", IsChildFriendly = false }
                  }
            },
            new NewsItem
            {
               Title = "NewsItem2",
               Url = new Url("https://news.com/NewsItem2"),
               Images =
                  new[] { new NewsImage { ImageName = "Item2Image1" } }
            }
         };

         #endregion

         var news = theNews.ToObservable();
         var newsImages =
            from newsItem in news
            from img in newsItem.Images
            where img.IsChildFriendly
            select new NewImageViewModel
            {
               ItemUrl = newsItem.Url,
               NewsImage = img
            };

         newsImages
            .Subscribe(AddToHeadlines);
      }

      private static void ProcessingTheSourceAndTheResult()
      {
         Demo.DisplayHeader("The SelectMany operator - processing the source item and the result item");

         #region creating collection of news items

         var theNews = new[]
         {
            new NewsItem
            {
               Title = "NewsItem1",
               Url = new Url("https://news.com/NewsItem1"),
               Images =
                  new[]
                  {
                     new NewsImage { ImageName = "Item1Image1" },
                     new NewsImage { ImageName = "Item1Image2", IsChildFriendly = false }
                  }
            },
            new NewsItem
            {
               Title = "NewsItem2",
               Url = new Url("https://news.com/NewsItem2"),
               Images =
                  new[] { new NewsImage { ImageName = "Item2Image1" } }
            }
         };

         #endregion

         var news = theNews.ToObservable();

         news.SelectMany(newsItem => newsItem.Images,
               (newsItem, img) => new NewImageViewModel
               {
                  ItemUrl = newsItem.Url,
                  NewsImage = img
               })
            .Where(vm => vm.NewsImage.IsChildFriendly)
            .Subscribe(AddToHeadlines);
      }

      private static void SimpleCollectionsFlattening()
      {
         Demo.DisplayHeader("The SelectMany operator - flattening collections of images from many news items");

         var theNews = new[]
         {
            new NewsItem
            {
               Title = "NewsItem1",
               Images =
                  new[]
                  {
                     new NewsImage { ImageName = "Item1Image1" },
                     new NewsImage { ImageName = "Item1Image2", IsChildFriendly = false }
                  }
            },
            new NewsItem
            {
               Title = "NewsItem2",
               Images =
                  new[] { new NewsImage { ImageName = "Item2Image1" } }
            }
         };

         var news = theNews.ToObservable();

         news.SelectMany(newsItem => newsItem.Images)
            .Where(img => img.IsChildFriendly)
            .Subscribe(AddToHeadlines);
      }

      private static void AddToHeadlines(NewsImage img)
      {
         Console.WriteLine("News headline image: {0}", img.ImageName);
      }

      private static void AddToHeadlines(NewImageViewModel img)
      {
         AddToHeadlines(img.NewsImage);
      }
   }

   public class NewsItem
   {
      public string Title { get; set; }
      public Url Url { get; set; }
      public IEnumerable<NewsImage> Images { get; set; }

      public override string ToString() => Title;
   }

   public class NewsImage
   {
      public NewsImage(bool isChildFriendly = true) => IsChildFriendly = isChildFriendly;

      public string ImageName { get; set; }
      public bool IsChildFriendly { get; set; }
   }

   internal class NewImageViewModel
   {
      public NewsImage NewsImage { get; set; }
      public Url ItemUrl { get; set; }
   }
}