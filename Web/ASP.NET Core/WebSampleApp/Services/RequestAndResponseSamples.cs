﻿using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WebSampleApp.Extensions;

namespace WebSampleApp.Services
{
   public class RequestAndResponseSamples
   {
      public static string GetRequestInformation(HttpRequest request)
      {
         StringBuilder sb = new();
         sb.Append("scheme".Div(request.Scheme));
         sb.Append("host".Div(request.Host.HasValue ? request.Host.Value : "no host"));
         sb.Append("path".Div(request.Path));
         sb.Append("query string".Div(request.QueryString.HasValue ? request.QueryString.Value! : "no query string"));
         sb.Append("method".Div(request.Method));
         sb.Append("protocol".Div(request.Protocol));

         return sb.ToString();
      }

      public static string GetHeaderInformation(HttpRequest request)
      {
         StringBuilder sb = new();
         foreach (var header in request.Headers)
         {
            sb.Append(header.Key.Div(string.Join("; ", header.Value)));
         }

         return sb.ToString();
      }

      public static string QueryParameters(HttpRequest request)
      {
         string xtext = request.Query["x"];
         string ytext = request.Query["y"];

         if (xtext is null || ytext is null)
         {
            return "x and y must be set".Div();
         }

         if (!int.TryParse(xtext, out var x))
         {
            return $"Error parsing {xtext}".Div();
         }

         if (!int.TryParse(ytext, out var y))
         {
            return $"Error parsing {ytext}".Div();
         }

         return $"{x} + {y} = {x + y}".Div();
      }

      public static string Content(HttpRequest request) => request.Query["data"];

      public static string Form(HttpRequest request) =>
         request.Method switch
         {
            "GET" => GetForm(),
            "POST" => ShowForm(request),
            _ => string.Empty
         };

      private static string GetForm() =>
         "<form method=\"post\" action=\"/randr/form\">" +
         "<input type=\"text\" name=\"text1\" />" +
         "<input type=\"submit\" value=\"Submit\" />" +
         "</form>";

      private static string ShowForm(HttpRequest request)
      {
         StringBuilder sb = new();
         if (request.HasFormContentType)
         {
            var coll = request.Form;
            foreach (var key in coll.Keys)
            {
               sb.Append(key.Div(HtmlEncoder.Default.Encode(coll[key])));
            }

            return sb.ToString();
         }

         return "no form".Div();
      }

      public static string WriteCookie(HttpResponse response)
      {
         response.Cookies.Append("color", "red",
            new CookieOptions
            {
               Path = "/randr",
               Expires = DateTime.Now.AddDays(1)
            });

         return "cookie written".Div();
      }

      public static string ReadCookie(HttpRequest request)
      {
         StringBuilder sb = new();
         var cookies = request.Cookies;
         foreach (var key in cookies.Keys)
         {
            sb.Append(key.Div(cookies[key] ?? string.Empty));
         }

         return sb.ToString();
      }

      public static string GetJson(HttpResponse response)
      {
         var b = new
         {
            Title = "Professional C# and .NET - 2021 Edition",
            Publisher = "Wiley",
            Author = "Christian Nagel"
         };
         var json = JsonSerializer.Serialize(b);
         response.ContentType = "application/json";

         return json;
      }
   }
}