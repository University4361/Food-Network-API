using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreWebAPI.Migrations
{
    public partial class UpdateCourier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courier",
                newName: "ID");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Courier",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Courier",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courier",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Courier",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Courier",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "Courier",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Courier");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Courier",
                newName: "Id");
        }
    }
}
