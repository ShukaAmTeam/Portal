using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Data
{
    public static class ApiStoreClientConfig
    {
        public static string Uri = "http://localhost:2448/";
        public static string AcceptType = "application/json";
        private static Dictionary<string, string> _routePrefixDictionary;
        public static string ApiUserName = "ApiUserName";
        public static string ApiPassword = "ApiPassword";
              

        public static string GetRoutPrefix(string key)
        {
            key = key.ToLower();

            if (_routePrefixDictionary == null)
            {
                InitRoutePrefix();
            }

            if (_routePrefixDictionary.ContainsKey(key))
                return _routePrefixDictionary[key];

            return null;
        }

        private static void InitRoutePrefix()
        {
            _routePrefixDictionary = new Dictionary<string, string>
            {
                {"product", "api/products" },
                {"store", "api/store" },
            };
        }
    }
}
