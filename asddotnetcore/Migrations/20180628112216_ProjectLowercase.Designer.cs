﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace asddotnetcore.Migrations
{
    [DbContext(typeof(MyWebApiContext))]
    [Migration("20180628112216_ProjectLowercase")]
    partial class ProjectLowercase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("MyWebApi.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .HasMaxLength(200);

                    b.Property<string>("details")
                        .HasMaxLength(1000);

                    b.Property<string[]>("extraimg");

                    b.Property<string>("imgAlt");

                    b.Property<string>("imgUrl");

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MyWebApi.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tools");
                });
#pragma warning restore 612, 618
        }
    }
}
