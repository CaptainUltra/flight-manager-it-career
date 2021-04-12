using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class FixPassengerReservationRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengersReservations_Reservations_PassengerId",
                table: "PassengersReservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cdb059b-02cf-43a9-b5b5-a3a4c63a77e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fddb8821-a4c5-40ad-a922-043bae88ca9f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8dd3b2e-920a-4d3d-ace1-c4de3f5b661f", "81dbf9ec-f225-4d4c-b04f-f9cbd35b2794", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90b1907a-f51f-4aeb-b95f-d202e04f59ca", "c27f46fe-0c21-4d6d-a046-6be2647e608f", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_PassengersReservations_ReservationId",
                table: "PassengersReservations",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengersReservations_Reservations_ReservationId",
                table: "PassengersReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengersReservations_Reservations_ReservationId",
                table: "PassengersReservations");

            migrationBuilder.DropIndex(
                name: "IX_PassengersReservations_ReservationId",
                table: "PassengersReservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90b1907a-f51f-4aeb-b95f-d202e04f59ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8dd3b2e-920a-4d3d-ace1-c4de3f5b661f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cdb059b-02cf-43a9-b5b5-a3a4c63a77e5", "b0de752b-a211-4db2-a2e4-473e6e2e3682", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fddb8821-a4c5-40ad-a922-043bae88ca9f", "31a4a6be-26be-4b8f-b4de-17a31b2c4042", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_PassengersReservations_Reservations_PassengerId",
                table: "PassengersReservations",
                column: "PassengerId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
