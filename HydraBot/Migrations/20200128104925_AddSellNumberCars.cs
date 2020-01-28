using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class AddSellNumberCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellNumbers",
                columns: table => new
                {
                    OperationId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<long>(nullable: false),
                    NumberId = table.Column<long>(nullable: false),
                    BuynerId = table.Column<long>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    IsClose = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellNumbers", x => x.OperationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellNumbers");
        }
    }
}
