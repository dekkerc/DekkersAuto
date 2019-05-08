using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Web.Data.Migrations
{
    public partial class removecar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Cars_CarId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "CarOptions");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CarId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriveTrain",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Kilometers",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Listings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Listings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListingOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ListingId = table.Column<Guid>(nullable: false),
                    OptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingOptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingOptions");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Doors",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "DriveTrain",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Kilometers",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Listings");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Listings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CarOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false),
                    OptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BodyType = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Doors = table.Column<int>(nullable: true),
                    DriveTrain = table.Column<string>(nullable: true),
                    FuelType = table.Column<string>(nullable: true),
                    Kilometers = table.Column<int>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Seats = table.Column<int>(nullable: true),
                    Transmission = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarId",
                table: "Listings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Cars_CarId",
                table: "Listings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
