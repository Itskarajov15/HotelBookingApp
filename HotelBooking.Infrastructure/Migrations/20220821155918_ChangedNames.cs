using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Data.Migrations
{
    public partial class ChangedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "RoomTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "RoomImages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "HotelName",
                table: "Hotels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "HotelImages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "CountryName",
                table: "Countries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Cities",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomTypes",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "RoomImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hotels",
                newName: "HotelName");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "HotelImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Countries",
                newName: "CountryName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "CityName");
        }
    }
}
