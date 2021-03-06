﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace asddotnetcore.Migrations
{
    [DbContext(typeof(MyWebApiContext))]
    partial class MyWebApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Details")
                        .HasMaxLength(512);

                    b.Property<List<string>>("ExtraUrls");

                    b.Property<List<string>>("Extraimg");

                    b.Property<string>("ImgAlt");

                    b.Property<string>("ImgUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<List<int>>("Tools");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MyWebApi.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tools");
                });
#pragma warning restore 612, 618
        }
    }
}
