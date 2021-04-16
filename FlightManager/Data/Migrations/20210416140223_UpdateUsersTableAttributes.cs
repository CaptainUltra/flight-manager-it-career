using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class UpdateUsersTableAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PersonalNo",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f9b82f9-227c-41a0-88e4-114db5caabc7", "687a1d5c-2a11-4eb6-898a-446215be39c1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "098aae73-0cca-4cbb-82a6-b2ee7ea3c141", "133ff7a1-b0ec-449d-92b0-3c20100ccba5", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "098aae73-0cca-4cbb-82a6-b2ee7ea3c141");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f9b82f9-227c-41a0-88e4-114db5caabc7");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e4d8cad-16e7-4132-b43a-7c5ed1e50dd8", "eaa04697-622c-4718-981b-764471127cba", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b6c88b1-9e42-49e2-8447-09c16c1a69c5", "1194c3d2-ce34-4c41-bc3a-382a3aa175c6", "Employee", "EMPLOYEE" });
        }
    }
}
