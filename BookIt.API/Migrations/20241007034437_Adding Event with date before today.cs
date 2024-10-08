using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingEventwithdatebeforetoday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("f09ce828-2207-4927-be79-459bf96ee99f"));

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "event_id", "artist", "category", "date", "description", "end_time", "event_name", "location", "price", "start_time" },
                values: new object[] { new Guid("9443d1cc-c761-4984-a1a8-837adfde4387"), "Parmish Verma", "Festive celebration", new DateOnly(2024, 10, 1), "Diwali Celebration", new TimeOnly(10, 0, 0), "Pre-Diwali Bash", "Delhi", 1000.0, new TimeOnly(9, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("9443d1cc-c761-4984-a1a8-837adfde4387"));

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "event_id", "artist", "category", "date", "description", "end_time", "event_name", "location", "price", "start_time" },
                values: new object[] { new Guid("f09ce828-2207-4927-be79-459bf96ee99f"), "Gaurav Kapoor", "Comedy Shows", new DateOnly(2024, 10, 10), "Laughter therapy", new TimeOnly(10, 0, 0), "Comedy Nights", "Noida", 2000.0, new TimeOnly(9, 0, 0) });
        }
    }
}
