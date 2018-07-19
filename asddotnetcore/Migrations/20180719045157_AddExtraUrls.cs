using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class AddExtraUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "ExtraUrls",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraUrls",
                table: "Projects");
        }
    }
}
