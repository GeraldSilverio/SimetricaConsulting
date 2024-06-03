using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTaskStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TaskStatus_IdTaskStatus",
                        column: x => x.IdTaskStatus,
                        principalTable: "TaskStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3209), 1, "N/A", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3224), "Nueva" },
                    { 2, "Admin", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3226), 1, "N/A", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3226), "En proceso" },
                    { 3, "Admin", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3228), 1, "N/A", new DateTime(2024, 6, 3, 8, 3, 38, 808, DateTimeKind.Local).AddTicks(3228), "Completada" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdTaskStatus",
                table: "Task",
                column: "IdTaskStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatus_Name",
                table: "TaskStatus",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "TaskStatus");
        }
    }
}
