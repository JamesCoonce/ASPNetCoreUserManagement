using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETFundamentals.Data.Migrations
{
    public partial class TicketMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "universityStatus",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "universityStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
