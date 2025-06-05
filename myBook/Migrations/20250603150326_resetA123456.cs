using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBook.Migrations
{
    /// <inheritdoc />
    public partial class resetA123456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sachs_Bookshelves_BookshelfId",
                table: "Sachs");

            migrationBuilder.DropIndex(
                name: "IX_Sachs_BookshelfId",
                table: "Sachs");

            migrationBuilder.DropColumn(
                name: "BookshelfId",
                table: "Sachs");

            migrationBuilder.AddColumn<string>(
                name: "MaSach",
                table: "Bookshelves",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelves_MaSach",
                table: "Bookshelves",
                column: "MaSach");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookshelves_Sachs_MaSach",
                table: "Bookshelves",
                column: "MaSach",
                principalTable: "Sachs",
                principalColumn: "MaSach",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookshelves_Sachs_MaSach",
                table: "Bookshelves");

            migrationBuilder.DropIndex(
                name: "IX_Bookshelves_MaSach",
                table: "Bookshelves");

            migrationBuilder.DropColumn(
                name: "MaSach",
                table: "Bookshelves");

            migrationBuilder.AddColumn<int>(
                name: "BookshelfId",
                table: "Sachs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sachs_BookshelfId",
                table: "Sachs",
                column: "BookshelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sachs_Bookshelves_BookshelfId",
                table: "Sachs",
                column: "BookshelfId",
                principalTable: "Bookshelves",
                principalColumn: "Id");
        }
    }
}
