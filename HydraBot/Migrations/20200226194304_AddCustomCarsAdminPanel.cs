using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class AddCustomCarsAdminPanel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomCars",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<long>(nullable: false),
                    Weight = table.Column<long>(nullable: false),
                    IsAvaliable = table.Column<bool>(nullable: false),
                    IsModerate = table.Column<bool>(nullable: false),
                    CarDatabaseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCars");
        }
    }
}
