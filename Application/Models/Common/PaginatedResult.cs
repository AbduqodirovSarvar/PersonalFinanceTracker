namespace Application.Models.Common
{
    public class PaginatedResult<TEntityViewModel> : Result<IEnumerable<TEntityViewModel>>
        where TEntityViewModel : BaseViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public static PaginatedResult<TEntityViewModel> Ok(IEnumerable<TEntityViewModel> data, int pageIndex, int pageSize, int totalItems)
            => new()
            {
                Success = true,
                Data = data,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems
            };

        public new static PaginatedResult<TEntityViewModel> Fail(string message) => new()
        {
            Success = false,
            Message = message,
            Data = [],
            PageIndex = 0,
            PageSize = 0,
            TotalItems = 0
        };
    }
}
