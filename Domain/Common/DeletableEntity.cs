using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public record DeletableEntity : IDeletableEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
