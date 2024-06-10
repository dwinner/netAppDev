using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class ApplicationSettings
    {
        public ConnectionStringSettings GetConnectionSetring(string key)
        {
            var value = ConfigurationManager.ConnectionStrings[key];
            if (value == null)
            {
                throw new Exception(string.Format("{0} not provided in the application configuration.", key));
            }
            return value;
        }
    }
}
