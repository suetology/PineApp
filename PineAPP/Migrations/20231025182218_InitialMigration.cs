using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PineAPP.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPersonal = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    Correct = table.Column<int>(type: "int", nullable: false),
                    Wrong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Front = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Back = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Examples = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    TotalCardsInDeck = table.Column<int>(type: "int", nullable: false),
                    CurrentCardIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "Id", "Correct", "CreatorId", "Description", "IsPersonal", "Name", "Wrong" },
                values: new object[,]
                {
                    { 1, 0, 1, "A few cards to test your basic math skills", false, "Simple Math (Community)", 0 },
                    { 2, 0, 1, "A few cards to test your basic math skills", true, "Simple Math (Personal)", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "admin", "admin" },
                    { 2, "vardenis.pavardenis@gmail.com", "testavicius", "testas" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Back", "CurrentCardIndex", "DeckId", "Examples", "Front", "TotalCardsInDeck" },
                values: new object[,]
                {
                    { 1, "4", 0, 1, "", "2 + 2 = ?", 0 },
                    { 2, "3", 0, 1, "", "5 - 2 = ?", 0 },
                    { 3, "12", 0, 1, "", "4 * 3 = ?", 0 },
                    { 4, "4", 0, 2, "", "2 + 2 = ?", 0 },
                    { 5, "3", 0, 2, "", "5 - 2 = ?", 0 },
                    { 6, "12", 0, 2, "", "4 * 3 = ?", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DeckId",
                table: "Cards",
                column: "DeckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Decks");
        }
    }
}
