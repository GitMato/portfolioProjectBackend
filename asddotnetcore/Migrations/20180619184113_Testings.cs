using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class Testings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Projects_ProjectId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ProjectId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tools");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Tools",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ProjectId",
                table: "Tools",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Projects_ProjectId",
                table: "Tools",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
