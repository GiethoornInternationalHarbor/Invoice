using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InvoiceService.Infrastructure.Migrations
{
    public partial class Updated_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipServices_Ships_ShipId",
                table: "ShipServices");

            migrationBuilder.DropIndex(
                name: "IX_ShipServices_ShipId",
                table: "ShipServices");

            migrationBuilder.DropColumn(
                name: "ShipId",
                table: "ShipServices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShipId",
                table: "ShipServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipServices_ShipId",
                table: "ShipServices",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipServices_Ships_ShipId",
                table: "ShipServices",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
