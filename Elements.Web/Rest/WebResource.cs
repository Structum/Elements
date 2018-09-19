using System.Net;

namespace Structum.Elements.Web.Rest
{
    /// <summary>
    ///     Defines the Web Resource class.
    /// </summary>
    public sealed class WebResource<T> where T : class
    {
        /// <summary>
        ///     The Resource Service URL.
        /// </summary>
        private readonly string _resourceSvcUrl;

        /// <summary>
        ///     Gets/Sets the resource data.
        /// </summary>
        /// <value>Resource data.</value>
        public T Data { get; set; }

        /// <summary>
		///     Initializes a new instance of the <see cref="WebResource{T}"/> class.
        /// </summary>
        /// <param name="resourceSvcUrl">Resource svc URL.</param>
        public WebResource(string resourceSvcUrl)
        {
            this._resourceSvcUrl = resourceSvcUrl;
        }

        /// <summary>
        ///     Save the resource.
        /// </summary>
        /// <param name="query">Save Query.</param>
        public void Save(string query="")
        {
			RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).PostJson(fastJSON.JSON.ToJSON(this.Data));
            if(!restResponse.Success) {
                throw new WebResourceException(restResponse.Errors);
            }

            if(!string.IsNullOrEmpty(restResponse.Body)) {
                this.Data = fastJSON.JSON.ToObject<T>(restResponse.Body);
            }
        }

        /// <summary>
        ///     Save the resource with different return type.
        /// </summary>
        /// <param name="query">Save Query.</param>
        /// <typeparam name="TR">Return Type.</typeparam>
        public TR Save<TR>(string query="") where TR : class
        {
			RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).PostJson(fastJSON.JSON.ToJSON(this.Data));
            if(!restResponse.Success) {
                throw new WebResourceException(restResponse.Errors);
            }

            if(string.IsNullOrEmpty(restResponse.Body)) {
                throw new WebResourceException("Query returned an empty set.");
            }

            return fastJSON.JSON.ToObject<TR>(restResponse.Body);
        }

        /// <summary>
        ///     Update the resource.
        /// </summary>
        /// <param name="query">Update Query.</param>
        public void Update(string query)
        {
			RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).PutJson(fastJSON.JSON.ToJSON(this.Data));
            if(!restResponse.Success) {
                throw new WebResourceException(restResponse.Errors);
            }

            if(!string.IsNullOrEmpty(restResponse.Body)) {
                this.Data = fastJSON.JSON.ToObject<T>(restResponse.Body);
            }
        }

        /// <summary>
        ///     Deletes the resource.
        /// </summary>
        /// <param name="query">Delete Query.</param>
        public void Delete(string query)
        {
            RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).Delete();
            if(!restResponse.Success) {
                throw new WebResourceException(restResponse.Errors);
            }

            if(!string.IsNullOrEmpty(restResponse.Body)) {
                this.Data = fastJSON.JSON.ToObject<T>(restResponse.Body);
            }
        }

        /// <summary>
        ///     Get the resource.
        /// </summary>
        /// <param name="query">Query.</param>
		/// <returns>The Resource Information if found, null otherwise.</returns>
        public T Get(string query)
        {
            RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).Get();
            if(!restResponse.Success && restResponse.StatusCode == HttpStatusCode.InternalServerError) {
                throw new WebResourceException(restResponse.Errors);
            }

            if (!restResponse.Success && restResponse.StatusCode != HttpStatusCode.OK) {
                return default(T);
            }

            if(string.IsNullOrEmpty(restResponse.Body)) {
				return default(T);
            }

            this.Data = fastJSON.JSON.ToObject<T>(restResponse.Body);

			return this.Data;
        }

        /// <summary>
        ///     Gets the list of resources.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>List of Resources</returns>
        public PagedList<T> GetList(string query)
        {
            RestResponse restResponse = new RestRequest(this._resourceSvcUrl+query).Get();
            if(!restResponse.Success) {
                throw new WebResourceException(restResponse.Errors);
            }

            if(string.IsNullOrEmpty(restResponse.Body)) {
                throw new WebResourceException("Query returned an empty set.");
            }

            return fastJSON.JSON.ToObject<PagedList<T>>(restResponse.Body);
        }
    }
}

