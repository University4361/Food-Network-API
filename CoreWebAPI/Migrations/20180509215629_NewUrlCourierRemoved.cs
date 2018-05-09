using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreWebAPI.Migrations
{
    public partial class NewUrlCourierRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewUrl",
                table: "Courier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewUrl",
                table: "Courier",
                maxLength: 256,
                nullable: true);
        }
    }
}
