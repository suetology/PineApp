using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PineAPP.Migrations
{
    /// <inheritdoc />
    public partial class changedDeckSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Decks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Simple Math (Community)");

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "Id", "CreatorId", "Description", "IsPersonal", "Name" },
                values: new object[] { 2, 1, "A few cards to test your basic math skills", true, "Simple Math (Personal)" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Back", "DeckId", "Examples", "Front" },
                values: new object[,]
                {
                    { 4, "4", 2, "", "2 + 2 = ?" },
                    { 5, "3", 2, "", "5 - 2 = ?" },
                    { 6, "12", 2, "", "4 * 3 = ?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Decks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Decks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Simple Math");
        }
    }
}
