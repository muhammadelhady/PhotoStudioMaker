using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMakerStudio.Migrations
{
    public partial class partneradded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartnerName = table.Column<string>(nullable: true),
                    PartnerWebsite = table.Column<string>(nullable: true),
                    LogoLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
