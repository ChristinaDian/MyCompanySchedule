using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCompanySchedule.Migrations
{
    public partial class UserToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TodolistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToTasks_Tasks_TodolistId",
                        column: x => x.TodolistId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToTasks_TodolistId",
                table: "UserToTasks",
                column: "TodolistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToTasks_UserId",
                table: "UserToTasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToTasks");
        }
    }
}
