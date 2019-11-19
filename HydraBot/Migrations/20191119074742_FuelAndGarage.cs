using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class FuelAndGarage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Fuel",
                table: "Garages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GarageModelId",
                table: "Garages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SelectCar",
                table: "Garages",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "GarageModelId",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "SelectCar",
                table: "Garages");
        }
    }
}
