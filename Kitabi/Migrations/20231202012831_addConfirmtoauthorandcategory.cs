using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kitabi.Migrations
{
    /// <inheritdoc />
    public partial class addConfirmtoauthorandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingBookAuthors");

            migrationBuilder.DropTable(
                name: "PendingBooks");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "ID",
                keyValue: 1,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "ID",
                keyValue: 2,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "ID",
                keyValue: 3,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 6,
                column: "Confirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 7,
                column: "Confirmed",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "PendingBookAuthors",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBookAuthors", x => new { x.BookID, x.AuthorID });
                });

            migrationBuilder.CreateTable(
                name: "PendingBooks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBooks", x => x.ID);
                });
        }
    }
}
