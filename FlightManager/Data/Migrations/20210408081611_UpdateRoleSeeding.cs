using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class UpdateRoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56b241eb-ea57-4c03-b58e-e4da854ff1af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "939297a8-9645-4651-b263-85da45925326");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cdb059b-02cf-43a9-b5b5-a3a4c63a77e5", "b0de752b-a211-4db2-a2e4-473e6e2e3682", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fddb8821-a4c5-40ad-a922-043bae88ca9f", "31a4a6be-26be-4b8f-b4de-17a31b2c4042", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "56b241eb-ea57-4c03-b58e-e4da854ff1af", "f8b108cc-41cd-46bc-96a9-e4634dd2601d", "QA", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "939297a8-9645-4651-b263-85da45925326", "a6491bbf-11dd-4a7c-a2c5-2f0d2b22301a", "Developer", null });
        }
    }
}
