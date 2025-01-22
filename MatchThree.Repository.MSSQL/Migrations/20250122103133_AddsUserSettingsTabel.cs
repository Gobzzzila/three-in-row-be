using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddsUserSettingsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Notifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Culture = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.Sql("INSERT INTO UserSettings (Id) SELECT Id FROM Users;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");
        }
    }
}
