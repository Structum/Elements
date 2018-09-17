using System.Collections.Generic;

namespace Structum.Elements
{
    /// <summary>
    ///     Defines a Paged List which represents a page of the set of items of a full list. This class
    ///     can be used to get sub-sets of items exemplified by pages.
    /// </summary>
    public sealed class PagedList<T>
    {
        /// <summary>
        ///     Gets or Sets the List containing the Data.
        /// </summary>
        /// <value><c>List&lt;T&gt;</c></value>
        public List<T> List { get; set; }

        /// <summary>
        ///     Gets or Sets the Start Position for this page.
        /// </summary>
        /// <value><c>int</c></value>
        public int Start { get; set; }

        /// <summary>
        ///     Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// G    ets or Sets the Total Count of records.
        /// </summary>
        /// <value><c>int</c></value>
        public long TotalCount { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        public PagedList()
        {
            this.List = new List<T>();
        }
    }
}

