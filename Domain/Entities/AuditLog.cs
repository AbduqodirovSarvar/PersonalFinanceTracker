using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record AuditLog : AudiTableEntity
    {
        public Guid? UserId { get; set; } = null;
        public User? User { get; set; }

        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
