using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETFundamentals.Data.Migrations
{
    public partial class AddUSerToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Tickets",
                newName: "ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets",
                newName: "IX_Tickets_ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ResourceId",
                table: "Tickets",
                column: "ResourceId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ResourceId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ResourceId",
                table: "Tickets",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ResourceId",
                table: "Tickets",
                newName: "IX_Tickets_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
