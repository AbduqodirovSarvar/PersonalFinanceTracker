using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Category : FullEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#000000";
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
