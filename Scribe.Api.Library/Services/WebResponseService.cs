using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scribe.Api.Library.Services
{
    public class WebResponseService
    {
        /// <summary>
        /// Method to get the reponse text sent from the server
        /// </summary>
        /// <param name="response">Response object from server</param>
        /// <returns>String message</returns>
        public string ProcessResponseText(WebResponse response)
        {
            Stream responseStream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                return sr.ReadToEnd();
            };
        }
    }
}