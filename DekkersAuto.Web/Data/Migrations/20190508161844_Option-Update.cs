using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Web.Data.Migrations
{
    public partial class OptionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Cars_CarId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_CarId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Options");

            migrationBuilder.AddColumn<Guid>(
                name: "OptionId",
                table: "Options",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Seats",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Kilometers",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Doors",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Options",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Seats",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Kilometers",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doors",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_CarId",
                table: "Options",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Cars_CarId",
                table: "Options",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
