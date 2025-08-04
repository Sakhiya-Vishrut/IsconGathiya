namespace IsconGathiya
{
    public class PaginationViewModel
    {
        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of each page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the name of the column used for sorting.
        /// </summary>
        public string? ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the direction of sorting (e.g., "asc" or "desc").
        /// </summary>
        public string? SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
