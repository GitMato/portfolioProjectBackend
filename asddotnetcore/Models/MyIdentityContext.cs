using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentity.Models
{
    public class MyIdentityContext : IdentityDbContext<ApiUser>
    {
        public MyIdentityContext(DbContextOptions options) : base(options) { }

        public DbSet <ApiUser> ApiUsers { get; set; }
    }
}
