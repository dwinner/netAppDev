using System;
using System.Globalization;
using System.Web.Services;

namespace SimpleWebService
{   
   [WebService(Namespace = "http://tempuri.org/")]
   [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
   [System.ComponentModel.ToolboxItem(false)]   
   [System.Web.Script.Services.ScriptService]
   public class SimpleWebServive : WebService
   {
      [WebMethod]
      public string SqrtWebMethod(int anInt)
      {
         return Math.Sqrt(Math.Abs(anInt)).ToString(CultureInfo.InvariantCulture);
      }
   }
}