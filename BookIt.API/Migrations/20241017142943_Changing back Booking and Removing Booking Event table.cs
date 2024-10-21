using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangingbackBookingandRemovingBookingEventtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking_Events");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "number_of_tickets",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "number_of_tickets",
                table: "Bookings");

            migrationBuilder.CreateTable(
                name: "Booking_Events",
                columns: table => new
                {
                    bookingEvent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    number_of_tickets = table.Column<int>(type: "int", nullable: false)
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
    }
}
