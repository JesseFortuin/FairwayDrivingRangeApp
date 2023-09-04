using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FairwayDrivingRange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedEntitiesWithOnModelCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CustomerInformation_CustomerInformationId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerInformationId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CustomerInformationId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CustomerInformation_CustomerId",
                table: "Transactions",
                column: "CustomerId",
                principalTable: "CustomerInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CustomerInformation_CustomerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "CustomerInformationId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerInformationId",
                table: "Transactions",
                column: "CustomerInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CustomerInformation_CustomerInformationId",
                table: "Transactions",
                column: "CustomerInformationId",
                principalTable: "CustomerInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
