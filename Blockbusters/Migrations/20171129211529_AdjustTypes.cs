using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blockbusters.Migrations
{
    public partial class AdjustTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoType",
                table: "Videos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoType",
                table: "Videos");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Videos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
