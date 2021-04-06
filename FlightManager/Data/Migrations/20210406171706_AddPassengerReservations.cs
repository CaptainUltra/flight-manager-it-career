using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class AddPassengerReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassengersReservations",
                columns: table => new
                {
                    PassengerId = table.Column<int>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    TicketType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengersReservations", x => new { x.PassengerId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_PassengersReservations_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengersReservations_Reservations_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengersReservations");
        }
    }
}
