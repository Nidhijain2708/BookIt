using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class Changingnameofbookingtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "booking_time",
                table: "Bookings",
                newName: "booking_dateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "booking_dateTime",
                table: "Bookings",
                newName: "booking_time");
        }
    }
}
