using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Scribe.Api.Library.Factories;
using Scribe.Api.Library.Settings;

namespace Scribe.Api.Library
{
    public class WebServiceDAO
    { 
        /// <summary>
        /// Method for sending request to server
        /// </summary>
        /// <param name="username">User name to use to access system</param>
        /// <param name="password">Password for account to access system</param>
        /// <param name="url">API url to use</param>
        /// <param name="method">Put, Get, Delete, Post</param>
        /// <returns>String message</returns>
        public string SendRequest(string username, string password, string url, string method)
        {
            WebResponseFactory factory = new WebResponseFactory();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                request.ContentType = ParameterContentTypeSettings.applicationjson;
                request.ContentLength = 0;

                Task<WebResponse> response = Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null);

                ///wait to get a response from the server
                do
                {
                    Thread.Sleep(GeneralSettings.WebRequestSleep);
                } while (response.IsCompleted == false);

                //We have a response, now process it.
                if(response.IsCompleted)
                {
                    return factory.ProcessResponse((HttpWebResponse)response.Result);
                }
                return factory.ProcessResponse(null);
            }
            catch
            {
                return factory.ProcessResponse(null);
            }
        }

        /// <summary>
        /// Method for sending request to server with a content body
        /// </summary>
        /// <param name="username">User name to use to access system</param>
        /// <param name="password">Password for account to access system</param>
        /// <param name="url">API url to use</param>
        /// <param name="method">Put, Get, Delete, Post</param>
        /// <param name="contentBody">JSON data to send in the body of the request.</param>
        /// <returns>String message</returns>
        public string SendRequest(string username, string password, string url, string method, string contentBody)
        {
            WebResponseFactory factory = new WebResponseFactory();
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] encodedData = encoding.GetBytes(contentBody);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                request.ContentType = ParameterContentTypeSettings.applicationjson;
                request.ContentLength = encodedData.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(encodedData, 0, encodedData.Length);
                requestStream.Close();

                Task<WebResponse> response = Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null);

                ///wait to get a response from the server
                do
                {
                    Thread.Sleep(GeneralSettings.WebRequestSleep);
                } while (response.IsCompleted == false);

                //We have a response, now process it.
                if (response.IsCompleted)
                {
                    return factory.ProcessResponse((HttpWebResponse)response.Result);
                }

                return factory.ProcessResponse(null);
            }
            catch
            {
                return factory.ProcessResponse(null);
            }
        }
    }
}