using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Windows;

namespace ActiveDirectory.UserSearch
{
   public partial class MainWindow
   {
      private string _username;
      private string _password;
      private string _hostname;
      private string _schemaNamingContext;
      private string _defaultNamingContext;

      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnLoadProperties(object sender, RoutedEventArgs e)
      {
         try
         {
            SetLogonInformation();

            SetNamingContext();

            SetUserProperties(_schemaNamingContext);
         }
         catch (Exception ex)
         {
            MessageBox.Show(String.Format("check your input! {0}", ex.Message));
         }
      }

      private void SetUserProperties(string schemaNamingContext)
      {
         var properties = from p in GetSchemaProperties(schemaNamingContext, "User").Concat(
                              GetSchemaProperties(schemaNamingContext, "Organizational-Person")).Concat(
                              GetSchemaProperties(schemaNamingContext, "Top"))
                          orderby p
                          select p;

         listBoxProperties.DataContext = properties;
      }

      private IEnumerable<string> GetSchemaProperties(string schemaNamingContext, string objectType)
      {
         IEnumerable<string> data;
         using (var de = new DirectoryEntry())
         {
            de.Username = _username;
            de.Password = _password;

            de.Path = String.Format("LDAP://{0}CN={1},{2}", _hostname, objectType, schemaNamingContext);

            PropertyValueCollection values = de.Properties["systemMayContain"];
            data = from s in values.Cast<string>()
                   orderby s
                   select s;
         }
         return data;
      }

      private void SetNamingContext()
      {
         using (var de = new DirectoryEntry())
         {
            string path = "LDAP://" + _hostname + "rootDSE";
            de.Username = _username;
            de.Password = _password;
            de.Path = path;
            _schemaNamingContext = de.Properties["schemaNamingContext"][0].ToString();
            _defaultNamingContext = de.Properties["defaultNamingContext"][0].ToString();
         }
      }

      private void SetLogonInformation()
      {
         _username = (textUsername.Text == "" ? null : textUsername.Text);
         _password = (textPassword.Password == "" ? null : textPassword.Password);
         _hostname = textDc.Text;
         if (_hostname != "")
         {
            _hostname += "/";
         }
      }

      private void OnSearch(object sender, RoutedEventArgs e)
      {
         try
         {
            FillResult();
         }
         catch (Exception ex)
         {
            MessageBox.Show(String.Format("check your input! {0}", ex.Message));
         }
      }

      private void FillResult()
      {
         using (var root = new DirectoryEntry())
         {
            root.Username = _username;
            root.Password = _password;
            root.Path = String.Format("LDAP://{0}{1}", _hostname, _defaultNamingContext);
            using (var searcher = new DirectorySearcher())
            {
               searcher.SearchRoot = root;
               searcher.SearchScope = SearchScope.Subtree;
               searcher.Filter = textFilter.Text;
               searcher.PropertiesToLoad.AddRange(listBoxProperties.SelectedItems.Cast<string>().ToArray());

               SearchResultCollection results = searcher.FindAll();
               var summary = new StringBuilder();
               foreach (SearchResult result in results)
               {
                  foreach (string propName in result.Properties.PropertyNames)
                  {
                     foreach (object p in result.Properties[propName])
                     {
                        summary.AppendFormat(" {0}: {1}", propName, p);
                        summary.AppendLine();
                     }
                  }
                  summary.AppendLine();
               }
               textResult.Text = summary.ToString();
            }
         }
      }
   }
}
