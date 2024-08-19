using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class EnergiesTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaderboardMember",
                table: "LeaderboardMember");

            migrationBuilder.DropColumn(
                name: "LogoutAt",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "LeaderboardMember",
                newName: "LeaderboardMembers");

            migrationBuilder.RenameIndex(
                name: "IX_LeaderboardMember_League",
                table: "LeaderboardMembers",
                newName: "IX_LeaderboardMembers_League");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaderboardMembers",
                table: "LeaderboardMembers",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateTable(
                name: "Energies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CurrentReserve = table.Column<int>(type: "int", nullable: false),
                    MaxReserve = table.Column<int>(type: "int", nullable: false),
                    RecoveryLevel = table.Column<int>(type: "int", nullable: false),
                    AvailableEnergyDrinkAmount = table.Column<int>(type: "int", nullable: false),
                    PurchasableEnergyDrinkAmount = table.Column<int>(type: "int", nullable: false),
                    LastRecoveryStartTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Energies", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Energies_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Energies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaderboardMembers",
                table: "LeaderboardMembers");

            migrationBuilder.RenameTable(
                name: "LeaderboardMembers",
                newName: "LeaderboardMember");

            migrationBuilder.RenameIndex(
                name: "IX_LeaderboardMembers_League",
                table: "LeaderboardMember",
                newName: "IX_LeaderboardMember_League");

            migrationBuilder.AddColumn<DateTime>(
                name: "LogoutAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaderboardMember",
                table: "LeaderboardMember",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);
        }
    }
}
