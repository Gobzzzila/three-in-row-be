using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchThree.Repository.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddsPurchasedEnergyDrinkCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnergyDrinkCounter",
                table: "Energies",
                newName: "UsedEnergyDrinkCounter");

            migrationBuilder.AddColumn<int>(
                name: "PurchasedEnergyDrinkCounter",
                table: "Energies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedEnergyDrinkCounter",
                table: "Energies");

            migrationBuilder.RenameColumn(
                name: "UsedEnergyDrinkCounter",
                table: "Energies",
                newName: "EnergyDrinkCounter");
        }
    }
}
