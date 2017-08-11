using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Common
{
    public class Config
    {
        public static string ApiKey => ConfigurationManager.AppSettings["API_KEY"];
        public static string EndPointUrl => ConfigurationManager.AppSettings["ENDPOINT_URL"];
    }
}
