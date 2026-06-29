using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mona_Logistics_LTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateFromVolumeToLinearMeters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_VolumeM3",
                table: "LoadRequests");

            migrationBuilder.RenameColumn(
                name: "VolumeM3",
                table: "LoadRequests",
                newName: "LinearMeters");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests",
                sql: "LinearMeters > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests");

            migrationBuilder.RenameColumn(
                name: "LinearMeters",
                table: "LoadRequests",
                newName: "VolumeM3");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_VolumeM3",
                table: "LoadRequests",
                sql: "VolumeM3 > 0");
        }
    }
}