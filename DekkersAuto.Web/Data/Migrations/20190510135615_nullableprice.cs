using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Web.Data.Migrations
{
    public partial class nullableprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Listings",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Listings",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
