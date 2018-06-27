using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class ToolChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tools",
                newName: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tools",
                newName: "Name");
        }
    }
}
