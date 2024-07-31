using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class remove_type_stranger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StrangerFilterFilterID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StrangerFilters",
                columns: table => new
                {
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FindGener = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MaxAge = table.Column<int>(type: "int", nullable: false),
                    _FindRegion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentRoomRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrangerFilters", x => x.FilterID);
                    table.ForeignKey(
                        name: "FK_StrangerFilters_Rooms_CurrentRoomRoomId",
                        column: x => x.CurrentRoomRoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StrangerFilterFilterID",
                table: "AspNetUsers",
                column: "StrangerFilterFilterID");

            migrationBuilder.CreateIndex(
                name: "IX_StrangerFilters_CurrentRoomRoomId",
                table: "StrangerFilters",
                column: "CurrentRoomRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StrangerFilters_StrangerFilterFilterID",
                table: "AspNetUsers",
                column: "StrangerFilterFilterID",
                principalTable: "StrangerFilters",
                principalColumn: "FilterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StrangerFilters_StrangerFilterFilterID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "StrangerFilters");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StrangerFilterFilterID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StrangerFilterFilterID",
                table: "AspNetUsers");
        }
    }
}
