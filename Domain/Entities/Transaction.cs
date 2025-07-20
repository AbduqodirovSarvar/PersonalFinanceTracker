using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Transaction : FullEntity
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; } = TransactionType.None;
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string? Note { get; set; }

        /// <summary>
        /// (Concurrency Conflict) ni oldini oladi, ya'ni bir vaqtning o‘zida bir nechta foydalanuvchi ma’lumotni o‘zgartirib yuborishining oldini oladi.
        /// </summary>
        public byte[] RowVersion { get; set; } = [];
    }
}
