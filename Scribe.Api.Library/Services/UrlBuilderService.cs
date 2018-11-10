using System;
using System.Collections.Generic;

namespace Scribe.Api.Library.Services
{
    public class UrlBuilderService
    {
        /// <summary>
        /// Method to build the url we will use to make a request
        /// </summary>
        /// <param name="url">Base url plus appended request url</param>
        /// <param name="oPath">Values needed for the path in the url (before ?)</param>
        /// <param name="oQuery">Values needed for the query in the url (after ?)</param>
        /// <returns>Constructed url</returns>
        public string BuildUrl(string url, Dictionary<int, string> oPath, Dictionary<string, object> oQuery)
        {
            try
            {
                //Requried info check
                if (url == null) throw new NullReferenceException("Base url can't be null.", new Exception("BuildUrl"));
                //Loop to remove all {*} from the url.  Will replace them as found and in the order of items in the dictionary.
                if(oPath != null && oPath.Count != 0)
                {
                    foreach (KeyValuePair<int, string> value in oPath)
                    {
                        int start = url.IndexOf("{");
                        int end = url.IndexOf("}") + 1;
                        int length = end - start;
                        string substring = url.Substring(start, length);
                        url = url.Replace(substring, value.Value);
                    }
                }

                //Used to create the query part of the path.
                if(oQuery != null && oQuery.Count != 0)
                {
                    url = url + "?";
                    foreach(KeyValuePair<string, object> query in oQuery)
                    {
                        url = url + string.Format("{0}={1}&",query.Key,query.Value);
                    }
                    if (url.EndsWith("&"))
                    {
                        url = url.Substring(0, url.Length - 1);
                    }
                }

                return url;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
