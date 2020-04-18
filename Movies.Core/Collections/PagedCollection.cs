using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies.Core.Collections
{
    public class PagedCollection<T> : IPagedCollection<T>
    {
        private readonly IReadOnlyCollection<T> elements = null;

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }

        public int Count => elements.Count;

        public PagedCollection(IEnumerable<T> source, int pageNumber, int pageSize, int? totalCount = null)
        {
            elements = source?.ToList();
            Initialise(pageNumber, pageSize, totalCount);
        }

        public PagedCollection(IReadOnlyCollection<T> source, int pageNumber, int pageSize, int? totalCount = null)
        {
            elements = source;
            Initialise(pageNumber, pageSize, totalCount);
        }

        private void Initialise(int pageNumber, int pageSize, int? totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount ?? elements.Count;

            // Assuming 1 is the first page rather than zero. Determining whether there is a another page
            // based on the size all of the previous pages plus the size of the current page
            var totalResultsReturned = (PageSize * (PageNumber - 1)) + Count;
            HasNextPage = totalResultsReturned < TotalCount;
            HasPreviousPage = pageNumber > 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}