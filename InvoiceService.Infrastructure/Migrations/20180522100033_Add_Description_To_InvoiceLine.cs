using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InvoiceService.Infrastructure.Migrations
{
    public partial class Add_Description_To_InvoiceLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShipServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InvoiceLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShipServices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InvoiceLines");
        }
    }
}
