using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Structum.Elements.Extensions;
using System.Text.RegularExpressions;

namespace Structum.Elements.Web.Rest
{
    /// <summary>
    ///     Defines the Rest Request.
    /// </summary>
    public class RestRequest
    {
        /// <summary>
        ///     Cookie Domain RegEx Pattern.
        /// </summary>
        private static readonly Regex CookieExpirationPattern = new Regex("[e|E]xpires=(?<expires>.*?);");

        /// <summary>
        ///     Gets or sets the request query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; }

        /// <summary>
		///     Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="query">Request Query.</param>
        public RestRequest(string query)
        {
            if(string.IsNullOrEmpty(query)) {
                throw new ArgumentNullException(nameof(query));
            }

            this.Query = query;
        }

        /// <summary>
        ///     Execute a Post JSON request.
        /// </summary>
        /// <param name="json">Post Body in JSON format.</param>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <param name="cookies">Request Cookies.</param>
        /// <returns>Rest Response.</returns>
        public RestResponse PostJson(string json, Dictionary<string, string> headers=null, Dictionary<string,string> cookies=null)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Query);
            request.Method = "POST";
            request.ContentType = "application/json";

			if(headers != null) {
				foreach(string header in headers.Keys) {
					request.Headers[header] = headers[header];
				}
			}

            if (cookies != null) {
                request.CookieContainer = new CookieContainer();
                string domain = new Uri(this.Query).Host;
                foreach (string cookie in cookies.Keys) {
                    string[] cleanCookieValue = cookies[cookie].Split(new [] {';'}, 2);
                    request.CookieContainer.Add(new Cookie(cookie, cleanCookieValue[0]) { Domain = domain});
                }
            }

            if (string.IsNullOrEmpty(json)) {
                json = "{}";
            }
            byte[] data = Encoding.UTF8.GetBytes(json);
            using(Stream dataStream = request.GetRequestStream()) {
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
            }

            return CreateRestResponse(request);
        }

        /// <summary>
        ///     Executes a Post File Request.
        /// </summary>
        /// <param name="fileName">File Name.</param>
        /// <param name="fileContents">File Contents.</param>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <returns>Rest Response.</returns>
        public RestResponse PostFile(string fileName, Stream fileContents, Dictionary<string, string> headers=null)
        {
            string boundary = "------------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] headerBytes = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"post_file_request\"; filename=\""+fileName+"\"\r\nContent-Type:application/octet-stream\r\n\r\n");

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Query);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            if(headers != null) {
                foreach(string header in headers.Keys) {
                    request.Headers[header] = headers[header];
                }
            }

            using (Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Write(headerBytes, 0, headerBytes.Length);

                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = fileContents.Read(buffer, 0, buffer.Length)) != 0) {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Close();
            }

            return CreateRestResponse(request);
        }

        /// <summary>
        ///     Execute a Put JSON request.
        /// </summary>
        /// <param name="json">Post Body in JSON format.</param>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <returns>Rest Response.</returns>
        public RestResponse PutJson(string json, Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Query);
            request.Method = "PUT";
            request.ContentType = "application/json";

            if(headers != null) {
                foreach(string header in headers.Keys) {
                    request.Headers[header] = headers[header];
                }
            }

            if (string.IsNullOrEmpty(json)) {
                return CreateRestResponse(request);
            }

            byte[] data = Encoding.UTF8.GetBytes(json);
            using(Stream dataStream = request.GetRequestStream()) {
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
            }

            return CreateRestResponse(request);
        }

        /// <summary>
        ///     Executes a Put File Request.
        /// </summary>
        /// <param name="fileName">File Name.</param>
        /// <param name="fileContents">File Contents.</param>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <returns>Rest Response.</returns>
        public RestResponse PutFile(string fileName, Stream fileContents, Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create($"{this.Query}/{fileName}");
            request.Method = "PUT";
            request.KeepAlive = true;

            if(headers != null) {
                foreach(string header in headers.Keys) {
                    request.Headers[header] = headers[header];
                }
            }

            using (Stream requestStream = request.GetRequestStream()) {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = fileContents.Read(buffer, 0, buffer.Length)) != 0) {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                requestStream.Close();
            }

            return CreateRestResponse(request);
        }

        /// <summary>
        ///     Execute a Get request.
        /// </summary>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <returns>Rest Response.</returns>
        public RestResponse Get(Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Query);
            request.Method = "GET";
            request.ContentType = "application/json";

            if(headers != null) {
                foreach(string header in headers.Keys) {
                    request.Headers[header] = headers[header];
                }
            }

			return CreateRestResponse(request);
        }

        /// <summary>
        ///     Execute a Delete request.
        /// </summary>
        /// <param name="headers">Request Headers. (Optional)</param>
        /// <returns>Rest Response.</returns>
        public RestResponse Delete(Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Query);
            request.Method = "DELETE";
            request.ContentType = "application/json";

            if (headers == null) {
                return CreateRestResponse(request);
            }

            foreach(string header in headers.Keys) {
                request.Headers[header] = headers[header];
            }

            return CreateRestResponse(request);
        }

        /// <summary>
		///     Creates the rest response from the Http Web request.
        /// </summary>
		/// <param name="request">Http Web request.</param>
        /// <returns>Rest response.</returns>
        private static RestResponse CreateRestResponse(WebRequest request)
        {
            RestResponse restResponse = new RestResponse();

            try {
				using(HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
	                restResponse.StatusCode = response.StatusCode;
	                using (var responseStream = response.GetResponseStream ()) {
	                    if (responseStream == null) {
	                        restResponse.Success = false;
	                        restResponse.Errors = "The response was empty.";

	                        return restResponse;
	                    }
	                    using (var reader = new StreamReader (responseStream)) {
	                        restResponse.Body = reader.ReadToEnd();
	                        reader.Close();
	                    }
	                    responseStream.Close();
	                }

	                restResponse.Success = true;

				    if (response.Headers.AllKeys.Contains("Set-Cookie")) {
				        KeyValuePair<string, string> parsedCookie = ParseCookie(response.Headers["Set-Cookie"]);
                        if(parsedCookie.Key != null && parsedCookie.Value != null) {
                            restResponse.Cookies = new Dictionary<string, string> {{parsedCookie.Key, parsedCookie.Value}};
                        }
				    }
				    response.Close();
				}
			} catch(WebException webEx) {
                restResponse.Errors = $"{webEx.Stringify()}";
                restResponse.Success = false;

                var response = (HttpWebResponse) webEx.Response;
                if (response == null) {
                    restResponse.StatusCode = HttpStatusCode.InternalServerError;
                    return restResponse;
                }

                restResponse.StatusCode = response.StatusCode;
                using (var responseStream = response.GetResponseStream()) {
                    if (responseStream == null) {
                        return restResponse;
                    }

                    using (var reader = new StreamReader(responseStream, Encoding.UTF8)) {
                        restResponse.Errors += $"\n{reader.ReadToEnd()}";
                        reader.Close();
                    }
                    responseStream.Close();
                }
            } catch (Exception ex) {
                restResponse.Success = false;
                restResponse.Errors = ex.Stringify();
            }

            return restResponse;
        }

        /// <summary>
        ///     Parses and Returns a Cookie Value pair.
        /// </summary>
        /// <param name="setCookieHeader">Set-Cookie Header.</param>
        /// <returns>Cookie Value Pair.</returns>
        private static KeyValuePair<string, string> ParseCookie(string setCookieHeader)
        {
            string[] cookie = setCookieHeader.Split(new[] {'='}, 2);
            string expires = CookieExpirationPattern.Match(cookie[1]).Groups["expires"].Value;

            bool cookieHasExpired = expires == DateTime.MinValue.ToString("ddd, dd-MMM-yyyy HH:mm:ss 'GMT'");
            return !cookieHasExpired ? new KeyValuePair<string, string>(cookie[0], cookie[1]) : new KeyValuePair<string, string>();
        }
    }
}

