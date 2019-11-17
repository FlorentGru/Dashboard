using Microsoft.EntityFrameworkCore.Migrations;

namespace DEV_dashboard_2019.Migrations.WidgetConfigurationDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementConf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    SteamId = table.Column<string>(nullable: true),
                    AppId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementConf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendListConf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    SteamId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendListConf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieConf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    Search = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieConf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherConf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherConf", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementConf");

            migrationBuilder.DropTable(
                name: "FriendListConf");

            migrationBuilder.DropTable(
                name: "MovieConf");

            migrationBuilder.DropTable(
                name: "WeatherConf");
        }
    }
}
