using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldToFieldTabelAndRemoveCellsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.AddColumn<string>(
                name: "Field",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "Fields");

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CellType = table.Column<int>(type: "int", nullable: false),
                    CoordinateX = table.Column<int>(type: "int", nullable: false),
                    CoordinateY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cells_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_UserId",
                table: "Cells",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cells_UserId_CoordinateX_CoordinateY",
                table: "Cells",
                columns: new[] { "UserId", "CoordinateX", "CoordinateY" },
                unique: true);
        }
    }
}
