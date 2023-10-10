using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FairwayDrivingRange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookingTableColumnDateBookedToStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBooked",
                table: "Bookings",
                newName: "Start");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Bookings",
                newName: "DateBooked");
        }
    }
}
