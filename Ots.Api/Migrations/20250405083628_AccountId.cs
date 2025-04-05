using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ots.Api.Migrations.MsSql
{
    /// <inheritdoc />
    public partial class AccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FromAccountId",
                schema: "dbo",
                table: "EftTransaction",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_EftTransaction_FromAccountId",
                schema: "dbo",
                table: "EftTransaction",
                newName: "IX_EftTransaction_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "dbo",
                table: "EftTransaction",
                newName: "FromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_EftTransaction_AccountId",
                schema: "dbo",
                table: "EftTransaction",
                newName: "IX_EftTransaction_FromAccountId");
        }
    }
}
