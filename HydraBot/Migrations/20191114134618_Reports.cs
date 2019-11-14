using Microsoft.EntityFrameworkCore.Migrations;

namespace HydraBot.Migrations
{
    public partial class Reports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(nullable: true),
                    AnswerMessage = table.Column<string>(nullable: true),
                    FromId = table.Column<long>(nullable: false),
                    ModeratorId = table.Column<long>(nullable: false),
                    IsAnswered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
