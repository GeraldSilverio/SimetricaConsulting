using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    IdUser = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdTaskStatus = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                    { 1, "Admin", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8650), 1, "N/A", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8690), "Nueva" },
                    { 2, "Admin", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8693), 1, "N/A", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8695), "En proceso" },
                    { 3, "Admin", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8697), 1, "N/A", new DateTime(2024, 6, 4, 2, 15, 26, 41, DateTimeKind.Local).AddTicks(8699), "Completada" }
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
