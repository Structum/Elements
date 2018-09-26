using System.Collections.Generic;

namespace Structum.Elements
{
    /// <summary>
    ///     Defines a Paged List which represents a page of the set of items of a full list. This class
    ///     can be used to get sub-sets of items exemplified by pages.
    /// </summary>
    /// <remarks>
    ///     This class can be used to get data out of the database on page at a time; specially useful if you need
    ///     a data structure to display data on a grid that can be navigated by pages.
    /// </remarks>
    /// <example>
    ///     To create a page list with starting at the first page containing 30 items of a full set of a 100:
    ///     <code>
    ///     var myResultsPage1 = new PagedList&lt;string&gt; {
    ///         Start = 0, // First Page
    ///         PageSize = 30, // Contains 30 items.
    ///         TotalCount = 100 // Total size of set. This page contains 30 items of a 100.
    ///     }
    ///     </code>
    ///     <br/>
    ///     To create the second page of this same set:
    ///     <code>
    ///     var myResultsPage2 = new PagedList&lt;string&gt; {
    ///         Start = 31, // Second Page, starting after the first page.
    ///         PageSize = 30, // Contains 30 items.
    ///         TotalCount = 100 // Total size of set. This page contains 30 items of a 100.
    ///     }
    ///     </code>
    ///     <br/>
    ///     <code>
    ///     var myResultsPage3 = new PagedList&lt;string&gt; {
    ///         Start = 61, // Third Page, starting after the second page.
    ///         PageSize = 30, // Contains 30 items.
    ///         TotalCount = 100 // Total size of set. This page contains 30 items of a 100.
    ///     }
    ///     </code>
    /// </example>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public sealed class PagedList<T>
    {
        /// <summary>
        ///     Gets or Sets the List containing the Data.
        /// </summary>
        /// <value>List of items contained in the PagedList.</value>
        public List<T> List { get; set; }

        /// <summary>
        ///     Gets or Sets the Start Position for this page.
        /// </summary>
        /// <value>Integer that represents the start index of this page.</value>
        public int Start { get; set; }

        /// <summary>
        ///     Gets or sets the size of the page.The Page Size represents the maximum number of items that page list can hold.
        /// </summary>
        /// <value>Integer that represents the size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or Sets the Total Count of records.
        /// </summary>
        /// <value>Integer that represents the total count of objects.</value>
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

