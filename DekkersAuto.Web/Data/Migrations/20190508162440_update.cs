using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Web.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Options",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOptions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Options");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Options",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OptionId",
                table: "Options",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
