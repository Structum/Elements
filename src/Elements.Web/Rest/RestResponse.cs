using System.Collections.Generic;
using System.Net;

namespace Structum.Elements.Web.Rest
{
    /// <summary>
    ///     Provides a set of properties to hold the HTTP Response including errors and cookies.
    /// </summary>
    /// <remarks>
    ///     The <see cref="RestResponse"/> is a simple data structure which holds the information collected by the <see cref="RestRequest"/>
    ///     when performing an HTTP request.
    /// </remarks>
    public class RestResponse
    {
        /// <summary>
        ///     Gets/Sets the Success flag.
        /// </summary>
        /// <value><c>true</c> if the operation was successful, <c>false</c> otherwise.</value>
        public bool Success { get; set; }

        /// <summary>
        ///     Gets/Sets the errors.
        /// </summary>
        /// <value>Errors if errors occurred, <c>null</c> otherwise.</value>
        public string Errors { get; set; }

        /// <summary>
        ///     Gets/Sets the Response body.
        /// </summary>
        /// <value>The response body.</value>
        public string Body { get; set; }

        /// <summary>
        ///     Gets/Sets the Http status code.
        /// </summary>
        /// <value>The Http status code.</value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///     Gets/Sets the Response Cookies.
        /// </summary>
        public Dictionary<string, string> Cookies { get; set; }
    }
}

