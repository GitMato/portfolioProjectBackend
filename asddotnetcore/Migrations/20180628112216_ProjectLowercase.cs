using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class ProjectLowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "Projects",
                newName: "imgUrl");

            migrationBuilder.RenameColumn(
                name: "ImgAlt",
                table: "Projects",
                newName: "imgAlt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "imgUrl",
                table: "Projects",
                newName: "ImgUrl");

            migrationBuilder.RenameColumn(
                name: "imgAlt",
                table: "Projects",
                newName: "ImgAlt");
        }
    }
}
