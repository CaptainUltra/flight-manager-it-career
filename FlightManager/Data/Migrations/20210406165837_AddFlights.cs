using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class AddFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    DepartureDateTime = table.Column<DateTime>(nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(nullable: false),
                    AircraftType = table.Column<string>(nullable: true),
                    FlightNumber = table.Column<string>(nullable: true),
                    PilotName = table.Column<string>(nullable: true),
                    PassengerCapacity = table.Column<int>(nullable: false),
                    BusinessCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
