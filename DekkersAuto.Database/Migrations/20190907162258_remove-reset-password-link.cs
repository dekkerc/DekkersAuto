using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DekkersAuto.Database.Migrations
{
    public partial class removeresetpasswordlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetPasswordLinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResetPasswordLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordLinks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordLinks_UserId",
                table: "ResetPasswordLinks",
                column: "UserId");
        }
    }
}
