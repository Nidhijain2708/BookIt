using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_event_id1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "event_id1",
                table: "Bookings",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_event_id1",
                table: "Bookings",
                newName: "IX_Bookings_EventId");

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

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Bookings",
                newName: "event_id1");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                newName: "IX_Bookings_event_id1");

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_event_id1",
                table: "Bookings",
                column: "event_id1",
                principalTable: "Events",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
