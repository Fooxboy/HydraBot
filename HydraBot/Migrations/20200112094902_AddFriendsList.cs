using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class AddFriendsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Friends",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendsRequests",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friends",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FriendsRequests",
                table: "Users");
        }
    }
}
