using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core.Collections
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements the collection contains.</typeparam>
    public interface IPagedCollection<out T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Whether or not there is another page of results available.
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Whether or not there is a previous page of results available.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// One-based index of the current page.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Maximum number of elements in the current page.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Total number of elements available in the entire collection.
        /// </summary>
        int TotalCount { get; }
    }
}
