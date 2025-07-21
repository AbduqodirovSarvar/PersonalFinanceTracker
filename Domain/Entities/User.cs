using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record User : FullEntity
    {
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = null!;
        public Role Role { get; set; } = Role.None;

        public ICollection<AuditLog> AuditLogs { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
