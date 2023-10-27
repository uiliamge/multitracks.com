using System;

namespace Domain
{
    public class PaginationMetadata
    {

        public PaginationMetadata(int totalCount, int currentPage, int itemsPerPage, string sortColumn, OrderByEnum sortDirection)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)itemsPerPage);
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }

        public string SortColumn { get; set; }
        public OrderByEnum SortDirection { get; set; }
        public int CurrentPage { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;


    }
}
