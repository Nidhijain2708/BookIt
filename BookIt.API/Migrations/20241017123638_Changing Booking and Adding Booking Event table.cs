using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangingBookingandAddingBookingEventtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking_Events",
                columns: table => new
                {
                    bookingEvent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    number_of_tickets = table.Column<int>(type: "int", nullable: false),
                    eventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking_Events", x => x.bookingEvent_id);
                    table.ForeignKey(
                        name: "FK_Booking_Events_Bookings_bookingId",
                        column: x => x.bookingId,
                        principalTable: "Bookings",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Events_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Events_bookingId",
                table: "Booking_Events",
                column: "bookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Events_eventId",
                table: "Booking_Events",
                column: "eventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking_Events");
        }
    }
}
