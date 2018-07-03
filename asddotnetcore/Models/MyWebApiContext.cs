using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Models
{
    public class MyWebApiContext : DbContext
    {
        //public MyWebApiContext()
        //{
        //}

        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tool> Tools { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProjectToTool>()
        //        .HasKey(t => new { t.ProjectId, t.ToolId });

        //    //modelBuilder.Entity<Project>()
        //    //    .HasMany(z => z.Tools)
        //    //    .WithOne(z => z.Project)
        //    //    .HasForeignKey(f => f.ProjectId);

        //    //modelBuilder.Entity<Tool>()
        //    //    .HasMany(x => x.ProjectTool)
        //    //    .WithOne(x => x.Tool)
        //    //    .HasForeignKey(fk => fk.ToolId);


        //    //modelBuilder.Entity<ProjectTool>()
        //    //    .HasOne(e => e.Tool)
        //    //    .WithOne()
        //    //    .HasForeignKey<Tool>();

        //    //"'The property 'ToolId' on entity type 'ProjectTool' has a temporary value"

        //    //modelBuilder
        //    //.Entity<PRAT>()
        //    //.HasOne(e => e.VW_PRATICHE_CONTIPO)
        //    //.WithOne()
        //    //.HasForeignKey<VW_PRATICHE_CONTIPO>();

        //    //modelBuilder.Entity<ServiceArea>()
        //    //.HasMany(z => z.ServiceAreaToZipCodes)
        //    //.WithOne(z => z.ServiceArea)
        //    //.HasForeignKey(f => f.ServiceAreaId);

        //    //modelBuilder.Entity<ZipCode>()
        //    //    .HasMany(pt => pt.ServiceAreaToZipCodes)
        //    //    .WithOne(t => t.ZipCode)
        //    //    .HasForeignKey(pt => pt.ZipCodeId);

        //}



        //Package Manager console commands for migrations:
        // add-migration
        // update-database

        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
    }
}
