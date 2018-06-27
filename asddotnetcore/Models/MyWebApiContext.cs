using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Models
{
    public class MyWebApiContext : DbContext
    {
        public MyWebApiContext()
        {
        }

        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tool> Tools { get; set; }

        //Package Manager console commands for migrations:
        // add-migration
        // update-database

        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
    }
}
