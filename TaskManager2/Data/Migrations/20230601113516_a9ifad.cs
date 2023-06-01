using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager2.Data.Migrations
{
    public partial class a9ifad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkTask",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WorkTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTask_UserId1",
                table: "WorkTask",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTask_AspNetUsers_UserId1",
                table: "WorkTask",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTask_AspNetUsers_UserId1",
                table: "WorkTask");

            migrationBuilder.DropIndex(
                name: "IX_WorkTask_UserId1",
                table: "WorkTask");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkTask");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WorkTask");
        }
    }
}
