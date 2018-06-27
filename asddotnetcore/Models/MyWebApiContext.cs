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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //optionsBuilder.UseSqlServer(@"Host=localhost;Database=portfoliodb;Trusted_Connection=True;ConnectRetryCount=0;Username=postgres");
        //    optionsBuilder.UseSqlServer(@"User ID = postgres; Server = localhost; Port = 5432; Database = portfoliodb; Integrated Security = true; Pooling = true;");
            
        //}
    }
}
