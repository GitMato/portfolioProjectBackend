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
    [Migration("20180618104028_Initial")]
    partial class Initial
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

                    b.Property<string>("ImgAlt");

                    b.Property<string>("ImgUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("description");

                    b.Property<string>("details");

                    b.Property<string[]>("extraimg");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MyWebApi.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tool");
                });

            modelBuilder.Entity("MyWebApi.Models.Tool", b =>
                {
                    b.HasOne("MyWebApi.Models.Project")
                        .WithMany("tools")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
