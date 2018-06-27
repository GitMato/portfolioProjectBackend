using Microsoft.EntityFrameworkCore.Migrations;

namespace asddotnetcore.Migrations
{
    public partial class AddTools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tool_Projects_ProjectId",
                table: "Tool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tool",
                table: "Tool");

            migrationBuilder.RenameTable(
                name: "Tool",
                newName: "Tools");

            migrationBuilder.RenameIndex(
                name: "IX_Tool_ProjectId",
                table: "Tools",
                newName: "IX_Tools_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tools",
                table: "Tools",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Projects_ProjectId",
                table: "Tools",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Projects_ProjectId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tools",
                table: "Tools");

            migrationBuilder.RenameTable(
                name: "Tools",
                newName: "Tool");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_ProjectId",
                table: "Tool",
                newName: "IX_Tool_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tool",
                table: "Tool",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tool_Projects_ProjectId",
                table: "Tool",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
