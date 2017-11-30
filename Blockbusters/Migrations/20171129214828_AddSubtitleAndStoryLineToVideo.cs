using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blockbusters.Migrations
{
    public partial class AddSubtitleAndStoryLineToVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Storyline",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Videos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Storyline",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Videos");
        }
    }
}
