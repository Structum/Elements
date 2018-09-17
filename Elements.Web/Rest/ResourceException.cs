using System;

namespace Structum.Elements.Web.Rest
{
    /// <summary>
    ///     Defines the Resource Exception.
    /// </summary>
    public class ResourceException : Exception
    {
        /// <summary>
		///     Initializes a new instance of the <see cref="ResourceException"/> class.
        /// </summary>
        /// <param name="errors">Errors.</param>
        public ResourceException(string errors) : base(errors) {}
    }
}

