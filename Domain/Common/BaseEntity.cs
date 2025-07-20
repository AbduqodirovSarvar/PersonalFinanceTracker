using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract record BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
