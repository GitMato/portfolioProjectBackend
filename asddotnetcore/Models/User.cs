using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentity.Models
{
    public class ApiUser : IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
    }
}
