using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class Removingpaymentstatusandchangingtypeofbookingtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment_status",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "booking_time",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "booking_time",
                table: "Bookings",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "payment_status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
