using Application.Models.Common;
using Domain.Enums;

namespace Application.Models
{
    public class TransactionViewModel : BaseViewModel
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }

        public Guid UserId { get; set; }
        public UserViewModel? User { get; set; }

        public string? Note { get; set; }
    }
}
