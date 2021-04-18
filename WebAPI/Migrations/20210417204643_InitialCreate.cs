using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Toy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toy", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Toy",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 1, "Jack in the Box", 999 });

            migrationBuilder.InsertData(
                table: "Toy",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 2, "Buzz Lightyear", 2499 });

            migrationBuilder.InsertData(
                table: "Toy",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 3, "Etch-A-Sketch", 1998 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Toy");
        }
    }
}
