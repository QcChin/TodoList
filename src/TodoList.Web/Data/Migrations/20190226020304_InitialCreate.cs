using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Web.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Creator = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Updater = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TodoId = table.Column<Guid>(nullable: false),
                    Todo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Creator = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Updater = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Creator = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Updater = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    TaskTypeId = table.Column<Guid>(nullable: true),
                    Label = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    OffTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todo_TodoType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TodoType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TaskTypeId",
                table: "Todo",
                column: "TaskTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "TodoItem");

            migrationBuilder.DropTable(
                name: "TodoType");
        }
    }
}
