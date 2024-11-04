using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyLoginsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyLogins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LastExecuteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StreakCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogins", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyLogins");
        }
    }
}
