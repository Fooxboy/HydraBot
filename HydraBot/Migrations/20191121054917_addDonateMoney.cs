using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class addDonateMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DonateMoney",
                table: "Users",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonateMoney",
                table: "Users");
        }
    }
}
