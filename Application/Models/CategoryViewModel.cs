using Application.Models.Common;

namespace Application.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserViewModel? User { get; set; }

        public decimal? TotalAmount { get; set; } = null;
        public DateOnly? FromDate { get; set; } = null;
        public DateOnly? ToDate { get; set; } = null;

        public ICollection<TransactionViewModel> Transactions { get; set; } = [];
    }
}
