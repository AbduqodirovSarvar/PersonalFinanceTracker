using Application.Models.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserViewModel? User { get; set; }

        public ICollection<TransactionViewModel> Transactions { get; set; } = [];
    }
}
