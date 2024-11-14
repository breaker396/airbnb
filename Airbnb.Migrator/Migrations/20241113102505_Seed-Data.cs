using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airbnb.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Camping" },
                    { 2, "Icons" },
                    { 3, "CountrySide" },
                    { 4, "Amazing Views" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "USA" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "ID", "Code", "Name", "Symbol" },
                values: new object[] { 1, "USD", "US Dollar", "$" });
            migrationBuilder.Sql("SET IDENTITY_INSERT [User] OFF");
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Name", "Password" },
                values: new object[,]
                {
                    { 12345678999999998L, "Buyer", "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014" },
                    { 12345678999999999L, "Seller", "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "ID", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Texas" },
                    { 2, 1, "Florida" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "ID", "Adults", "CategoryId", "CheckinDate", "CheckoutDate", "CreatedBy", "CurrencyId", "DeleteBy", "Guests", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Price", "ProvinceId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1963614788781513L, 3, 4, new DateTime(2024, 11, 18, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4913), new DateTime(2024, 11, 22, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4913), 12345678999999999L, 1, 0L, 4, false, 0L, null, "City Views", 5.12m, 1, 3.56m, 12345678999999999L },
                    { 4193407947516112L, 5, 2, new DateTime(2024, 11, 15, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4904), new DateTime(2024, 11, 21, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4904), 12345678999999999L, 1, 0L, 10, false, 0L, null, "River Views", 2m, 2, 3.54m, 12345678999999999L },
                    { 6527511584915398L, 2, 3, new DateTime(2024, 11, 14, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4909), new DateTime(2024, 12, 3, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4910), 12345678999999999L, 1, 0L, 5, false, 0L, null, "Street Views", 3.33m, 1, 2.53m, 12345678999999999L },
                    { 8908078312198154L, 2, 1, new DateTime(2024, 11, 14, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4894), new DateTime(2024, 11, 23, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4897), 12345678999999999L, 1, 0L, 2, false, 0L, null, "Garden Views", 0.3m, 1, 4.82m, 12345678999999999L },
                    { 11174855181556517L, 6, 1, new DateTime(2024, 11, 22, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4916), new DateTime(2024, 12, 3, 10, 25, 5, 391, DateTimeKind.Utc).AddTicks(4916), 12345678999999999L, 1, 0L, 12, false, 0L, null, "Ocean Views", 9.34m, 2, 1.82m, 12345678999999999L }
                });
            migrationBuilder.Sql("SET IDENTITY_INSERT [User] ON");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "ID",
                keyValue: 1963614788781513L);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "ID",
                keyValue: 4193407947516112L);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "ID",
                keyValue: 6527511584915398L);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "ID",
                keyValue: 8908078312198154L);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "ID",
                keyValue: 11174855181556517L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "ID",
                keyValue: 12345678999999998L);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Province",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Province",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "ID",
                keyValue: 12345678999999999L);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
