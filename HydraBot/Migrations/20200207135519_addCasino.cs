using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class addCasino : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Chips",
                table: "Users",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chips",
                table: "Users");
        }
    }
}
