using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldElementsTabelAndRefactorOldVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldElementLevels");

            migrationBuilder.CreateTable(
                name: "FieldElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Element = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldElements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FieldLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Fields_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_UserId",
                table: "Cells",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldElements_UserId",
                table: "FieldElements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldElements_UserId_Element",
                table: "FieldElements",
                columns: new[] { "UserId", "Element" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldElements");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Cells_UserId",
                table: "Cells");

            migrationBuilder.CreateTable(
                name: "FieldElementLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FieldLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldElementLevels", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_FieldElementLevels_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });
        }
    }
}
