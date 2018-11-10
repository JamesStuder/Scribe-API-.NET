using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Scribe.Api.Library.Services;

namespace Scribe.Api.Library.Factories
{
    public class WebResponseFactory
    {
        /// <summary>
        /// Factory method to decide what to return based on the server reponse to the request.
        /// </summary>
        /// <param name="response">Response object received from server</param>
        /// <returns>String message</returns>
        public string ProcessResponse(HttpWebResponse response)
        {
            WebResponseService service = new WebResponseService();
            switch (response.StatusCode.ToString())
            {
                //200
                case "OK":
                    return service.ProcessResponseText(response);
                //201
                case "CREATED":
                    return "CREATED";
                //204
                case "NoContent":
                    return "NO CONTENT";
                //400
                case "BAD REQUEST":
                    return "BAD REQUEST";
                //401
                case "UNAUTHORIZED":
                    return "UNAUTHORIZED";
                //403
                case "FORBIDDEN":
                    return "FORBIDDEN";
                //404
                case "NOT FOUND":
                    return "NOT FOUND";
                //405
                case "METHOD NOT ALLOWED":
                    return "METHOD NOT ALLOWED";
                //409
                case "CONFLICT":
                    return "CONFLICT";
                //500
                case "INTERNAL SERVER ERROR":
                    return "INTERNAL SERVER ERROR";
                default:
                    return string.Empty;
            }
        }
    }
}
