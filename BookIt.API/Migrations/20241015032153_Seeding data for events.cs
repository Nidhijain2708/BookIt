using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataforevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("9443d1cc-c761-4984-a1a8-837adfde4387"));

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "event_id", "artist", "available_tickets", "capacity", "category", "date", "description", "end_time", "event_name", "location", "price", "start_time" },
                values: new object[,]
                {
                    { new Guid("1ccab91a-f452-417b-bb72-787aff994af8"), "Karan Aujla", 200, 200, "Music", new DateOnly(2025, 3, 1), "Experience the magic of Karaan Aujla live on his It Was All A Dream World Tour!", new TimeOnly(20, 0, 0), "It Was All A Dream", "Mumbai", 1999.0, new TimeOnly(19, 0, 0) },
                    { new Guid("7a08f46b-f298-47b5-a39e-f331f8ddc575"), "Manhar Seth", 60, 60, "Comedy", new DateOnly(2025, 1, 2), "Manhar Seth's pomedy set 'Main Shayar Toh Nahi' along with some crowdwork", new TimeOnly(10, 0, 0), "Main Shayar Toh Nahi ft Manhar Seth", "Mehendi Navaz Jung Hall: Ahemdabad", 399.0, new TimeOnly(9, 0, 0) },
                    { new Guid("897a64d7-94b2-428d-9548-1d9c4f78c65c"), "Kunal Kapur", 300, 300, "Food Fest", new DateOnly(2024, 11, 16), "The Happiest Food Festival 13.0", new TimeOnly(16, 0, 0), "Horn OK Please", "JLN Stadium, Gate No. 14, Delhi", 299.0, new TimeOnly(12, 0, 0) },
                    { new Guid("a5807ecd-be61-4845-8645-c88ac1e79e4b"), "All kids who join us", 50, 50, "Competition", new DateOnly(2024, 11, 2), "CoComelon - Family & Friends are coming to India for the first time…for a Play Date!", new TimeOnly(10, 0, 0), "Cocomelon Comes To Ahmedabad!", "Palladium Mall: Ahemdabad", 100.0, new TimeOnly(9, 0, 0) },
                    { new Guid("afb4c67b-f72c-4486-b86f-5690e4b6937f"), "John Flubber", 120, 120, "Acting", new DateOnly(2024, 12, 4), "Join Flubber, the renowned award-winning clown and creator of India's International Clown Festival, on another exhilarating adventure.", new TimeOnly(15, 0, 0), "International Clown Festival - Chennai", "Sri Mutha Venkatasubba Rao Concert Hall: Chennai", 800.0, new TimeOnly(13, 0, 0) },
                    { new Guid("b60c1f30-a2cc-4453-ae8e-d1ba4677d64e"), "Anubhav Singh Bassi", 70, 70, "Comedy", new DateOnly(2025, 2, 2), "After the great success of his previous show Bas kar bassi, Anubhav Singh Bassi is coming back to perform live on stage.", new TimeOnly(12, 0, 0), "Kisi Ko Batana Mat Ft. Anubhav Singh Bassi", "Delhi", 799.0, new TimeOnly(11, 0, 0) },
                    { new Guid("b8c62ec5-70b6-4618-a289-d357245c5dc0"), "Gaurav Gupta", 60, 60, "Comedy", new DateOnly(2024, 12, 10), "Diwali Celebration", new TimeOnly(11, 0, 0), "Gaurav Gupta Live", "Talkatora Stadium Delhi", 799.0, new TimeOnly(10, 0, 0) },
                    { new Guid("bcc8c341-ece2-4fec-81ca-3b7f31f3c195"), "Neha Dhupia", 100, 100, "Game", new DateOnly(2024, 12, 19), "Discover Connections in the Most Exciting Way (18+)", new TimeOnly(19, 0, 0), "Thrifty X Genz Strangers Meet", "Delhi", 599.0, new TimeOnly(13, 0, 0) },
                    { new Guid("e1772253-33d7-4cd0-a49b-aeeeae35ae29"), "Darshan Raval", 200, 200, "Music", new DateOnly(2025, 1, 10), "Can't wait to listen to Darshan Raval's new album 'Out Of Control'?", new TimeOnly(21, 0, 0), "'Out Of Control' with Blue Family", "Saket Social, Delhi", 2000.0, new TimeOnly(18, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("1ccab91a-f452-417b-bb72-787aff994af8"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("7a08f46b-f298-47b5-a39e-f331f8ddc575"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("897a64d7-94b2-428d-9548-1d9c4f78c65c"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("a5807ecd-be61-4845-8645-c88ac1e79e4b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("afb4c67b-f72c-4486-b86f-5690e4b6937f"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("b60c1f30-a2cc-4453-ae8e-d1ba4677d64e"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("b8c62ec5-70b6-4618-a289-d357245c5dc0"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("bcc8c341-ece2-4fec-81ca-3b7f31f3c195"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("e1772253-33d7-4cd0-a49b-aeeeae35ae29"));

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "event_id", "artist", "available_tickets", "capacity", "category", "date", "description", "end_time", "event_name", "location", "price", "start_time" },
                values: new object[] { new Guid("9443d1cc-c761-4984-a1a8-837adfde4387"), "Parmish Verma", 250, 250, "Festive celebration", new DateOnly(2024, 10, 1), "Diwali Celebration", new TimeOnly(10, 0, 0), "Pre-Diwali Bash", "Delhi", 1000.0, new TimeOnly(9, 0, 0) });
        }
    }
}
