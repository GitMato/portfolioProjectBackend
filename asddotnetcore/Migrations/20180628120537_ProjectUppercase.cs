using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class ProjectUppercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "extraimg",
                table: "Projects",
                newName: "Extraimg");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "Projects",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Projects",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Extraimg",
                table: "Projects",
                newName: "extraimg");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Projects",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "description");
        }
    }
}
