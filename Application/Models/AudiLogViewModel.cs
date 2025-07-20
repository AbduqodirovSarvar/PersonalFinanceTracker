using Application.Models.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AudiLogViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }
        public UserViewModel? User { get; set; }

        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
