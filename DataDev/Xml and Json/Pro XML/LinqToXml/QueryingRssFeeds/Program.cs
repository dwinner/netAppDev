/**
 * Запрашивание динамических XML-документов
 */

using System;
using System.Linq;
using System.Xml.Linq;

namespace QueryingRssFeeds
{
   internal static class Program
   {
      private const string RssFeedUrl = "http://geekswithblogs.net/evjen/Rss.aspx";

      private static void Main()
      {
         var rssFeeds = XDocument.Load(RssFeedUrl);
         var feedQuery = from feed in rssFeeds.Descendants("channel")
                         let titleEl = feed.Element("title")
                         let descriptionEl = feed.Element("description")
                         let linkEl = feed.Element("link")
                         select new
                         {
                            Title = titleEl != null ? titleEl.Value : string.Empty,
                            Description = descriptionEl != null ? descriptionEl.Value : string.Empty,
                            Link = linkEl != null ? linkEl.Value : string.Empty
                         };

         foreach (var feedItem in feedQuery)
         {
            Console.WriteLine("Title: {0}", feedItem.Title);
            Console.WriteLine("Description: {0}", feedItem.Description);
            Console.WriteLine("Link: {0}", feedItem.Link);
         }

         Console.WriteLine();

         try
         {
            var postQuery = from posts in rssFeeds.Descendants("item")
                            let titleEl = posts.Element("title")
                            let pubDateEl = posts.Element("pubDate")
                            where pubDateEl != null
                            let descriptionEl = posts.Element("description")
                            let commnetsEl = posts.Element("comments")
                            select new
                            {
                               Title = titleEl != null ? titleEl.Value : string.Empty,
                               Published = DateTime.Parse(pubDateEl.Value),
                               Description = descriptionEl != null ? descriptionEl.Value : string.Empty,
                               Comments = commnetsEl != null ? commnetsEl.Value : string.Empty
                            };

            foreach (var postItem in postQuery)
            {
               Console.WriteLine(postItem.Title);
            }
         }
         catch (FormatException formatException)
         {
            Console.WriteLine(formatException.Message);
         }
      }
   }
}