using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookIt.API.Migrations
{
    /// <inheritdoc />
    public partial class Addingcapacityandavailableticketsinevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "available_tickets",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "capacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "event_id",
                keyValue: new Guid("9443d1cc-c761-4984-a1a8-837adfde4387"),
                columns: new[] { "available_tickets", "capacity" },
                values: new object[] { 250, 250 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "available_tickets",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "capacity",
                table: "Events");
        }
    }
}
