using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Role
    {
        None = 0,
        User = 1,
        Admin = 2,
        Moderator = 4,
        SuperAdmin = 5
    }
}
