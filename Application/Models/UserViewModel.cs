using Application.Models.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models
{
    public class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
