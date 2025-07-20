using Application.Models.Common;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
