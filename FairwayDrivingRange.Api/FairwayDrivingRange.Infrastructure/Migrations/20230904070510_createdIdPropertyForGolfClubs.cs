using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FairwayDrivingRange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createdIdPropertyForGolfClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GolfClubs",
                table: "GolfClubs");

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "GolfClubs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GolfClubs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GolfClubs",
                table: "GolfClubs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GolfClubs",
                table: "GolfClubs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GolfClubs");

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "GolfClubs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GolfClubs",
                table: "GolfClubs",
                column: "SerialNumber");
        }
    }
}
