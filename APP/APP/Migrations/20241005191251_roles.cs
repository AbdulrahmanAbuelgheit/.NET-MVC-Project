using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APP.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "962f5dba-02bf-4649-b5ce-edeed50425a9", "eafd790b-c444-400a-a2aa-2a26b35ad6fe", "User", "user" },
                    { "9ee01475-3cf2-4d1b-b053-84cedd54d4a0", "4abeb5b8-dc59-4509-8acb-546541dacd7c", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "962f5dba-02bf-4649-b5ce-edeed50425a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ee01475-3cf2-4d1b-b053-84cedd54d4a0");
        }
    }
}
