using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class updatingtables4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    ticket_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticket_type_available_tickets = table.Column<int>(type: "int", nullable: false),
                    ticket_type_capacity = table.Column<int>(type: "int", nullable: false),
                    ticket_type_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticket_type_price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.ticket_type_id);
                    table.ForeignKey(
                        name: "FK_TicketTypes_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypes_eventId",
                table: "TicketTypes",
                column: "eventId");
        }
    }
}
