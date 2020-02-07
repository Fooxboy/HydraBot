using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class AddUnsubNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SubOnNews",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubOnNews",
                table: "Users");
        }
    }
}
