using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ListPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Price50 = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Price100 = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Billy Spark", "A calming journey through the woods, where every tree tells a story of peace and resilience.", "SWD9999001", 99m, 90m, 80m, 85m, "Whispers of the Forest" },
                    { 2, "Nancy Hoover", "An epic tale of hidden secrets that resurface to challenge the bonds of friendship and trust.", "CAW777777701", 40m, 30m, 20m, 25m, "Shadows of the Past" },
                    { 3, "Julian Button", "A futuristic adventure exploring the thin line between human dreams and technological reality.", "RITO5555501", 55m, 50m, 35m, 40m, "Echoes of Tomorrow" },
                    { 4, "Abby Muscles", "A thrilling story of explorers stumbling upon a forgotten land filled with mysteries and wonders.", "WS3333333301", 70m, 65m, 55m, 60m, "The Hidden Valley" },
                    { 5, "Ron Parker", "An inspiring voyage into space, following the courage of pioneers reaching for the unknown.", "SOTJ1111111101", 30m, 27m, 20m, 25m, "Journey to the Stars" },
                    { 6, "Laura Phantom", "A touching drama where every drop of rain carries a memory of love, loss, and hope.", "FOT000000001", 25m, 23m, 20m, 22m, "Voices in the Rain" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
