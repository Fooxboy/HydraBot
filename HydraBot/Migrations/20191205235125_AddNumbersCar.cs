using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class AddNumbersCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Numbers",
                table: "Garages",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "Cars",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "NumbersCars",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    CarId = table.Column<long>(nullable: false),
                    Owner = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumbersCars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumbersCars");

            migrationBuilder.DropColumn(
                name: "Numbers",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Cars");
        }
    }
}
