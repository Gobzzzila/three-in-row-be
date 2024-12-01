using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class DependencyChangeForFieldElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldElements_Users_UserId",
                table: "FieldElements");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldElements_Fields_UserId",
                table: "FieldElements",
                column: "UserId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldElements_Fields_UserId",
                table: "FieldElements");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldElements_Users_UserId",
                table: "FieldElements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
