using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class UpdateReservationsTableAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "545a6e9d-98a6-4f99-9eed-689103b14bce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "680c94e0-51d4-4594-9ab1-051c4a014970");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e4d8cad-16e7-4132-b43a-7c5ed1e50dd8", "eaa04697-622c-4718-981b-764471127cba", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b6c88b1-9e42-49e2-8447-09c16c1a69c5", "1194c3d2-ce34-4c41-bc3a-382a3aa175c6", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b6c88b1-9e42-49e2-8447-09c16c1a69c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e4d8cad-16e7-4132-b43a-7c5ed1e50dd8");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "680c94e0-51d4-4594-9ab1-051c4a014970", "0b312bfe-c3f8-428a-917a-f4f8af6b404e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "545a6e9d-98a6-4f99-9eed-689103b14bce", "006cfabc-d36c-40e4-9882-7b6008c895c1", "Employee", "EMPLOYEE" });
        }
    }
}
