using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBook.Migrations
{
    /// <inheritdoc />
    public partial class resetA1234567 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_ShoppingCarts_ShoppingCartId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetail_Sachs_MaSach",
                table: "ShoppingCartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetail_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartDetail",
                table: "ShoppingCartDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "ShoppingCartDetail",
                newName: "ShoppingCartDetails");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetail_ShoppingCartId",
                table: "ShoppingCartDetails",
                newName: "IX_ShoppingCartDetails_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetail_MaSach",
                table: "ShoppingCartDetails",
                newName: "IX_ShoppingCartDetails_MaSach");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_ShoppingCartId",
                table: "Payments",
                newName: "IX_Payments_ShoppingCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartDetails",
                table: "ShoppingCartDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ShoppingCarts_ShoppingCartId",
                table: "Payments",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_Sachs_MaSach",
                table: "ShoppingCartDetails",
                column: "MaSach",
                principalTable: "Sachs",
                principalColumn: "MaSach",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ShoppingCarts_ShoppingCartId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_Sachs_MaSach",
                table: "ShoppingCartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartDetails",
                table: "ShoppingCartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "ShoppingCartDetails",
                newName: "ShoppingCartDetail");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetails_ShoppingCartId",
                table: "ShoppingCartDetail",
                newName: "IX_ShoppingCartDetail_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetails_MaSach",
                table: "ShoppingCartDetail",
                newName: "IX_ShoppingCartDetail_MaSach");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ShoppingCartId",
                table: "Payment",
                newName: "IX_Payment_ShoppingCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartDetail",
                table: "ShoppingCartDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_ShoppingCarts_ShoppingCartId",
                table: "Payment",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetail_Sachs_MaSach",
                table: "ShoppingCartDetail",
                column: "MaSach",
                principalTable: "Sachs",
                principalColumn: "MaSach",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetail_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetail",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
