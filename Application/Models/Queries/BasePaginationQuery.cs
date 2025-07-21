namespace Application.Models.Queries
{
    public record BasePaginationQuery
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "desc";
    }
}
