using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FairwayDrivingRange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkedTransactionToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CustomerInformation_CustomerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "CustomerInformation");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Transactions",
                newName: "BookingId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BookingId",
                table: "Transactions",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_BookingId",
                table: "Transactions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_BookingId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BookingId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Transactions",
                newName: "CustomerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "CustomerInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
