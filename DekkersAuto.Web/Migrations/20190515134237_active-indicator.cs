using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Web.Migrations
{
    public partial class activeindicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Listings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Listings");
        }
    }
}
