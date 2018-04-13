using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETFundamentals.Data.Migrations
{
    public partial class UpdateTicketUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTicket");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "UserTicket",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTicket", x => new { x.UserId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_UserTicket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTicket_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTicket_TicketId",
                table: "UserTicket",
                column: "TicketId");
        }
    }
}
