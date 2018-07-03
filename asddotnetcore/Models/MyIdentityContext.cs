using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentity.Models
{
    public class MyIdentityContext : IdentityDbContext<Admin>
    {
        public MyIdentityContext(DbContextOptions options) : base(options) { }

        public DbSet <Admin> Admins { get; set; }
    }
}
