using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mona_Logistics_LTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedTruckAcaiabilityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TruckAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AvailableWeightKg = table.Column<double>(type: "float", nullable: false),
                    AvailableLinearMeters = table.Column<double>(type: "float", nullable: false),
                    AvailableVolumeM3 = table.Column<double>(type: "float", nullable: false),
                    CurrentLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    HasCooling = table.Column<bool>(type: "bit", nullable: false),
                    HasTailLift = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchedLoadRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckAvailabilities_Drivers_DriverId1",
                        column: x => x.DriverId1,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TruckAvailabilities_LoadRequests_MatchedLoadRequestId",
                        column: x => x.MatchedLoadRequestId,
                        principalTable: "LoadRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TruckAvailabilities_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TruckAvailabilityDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckAvailabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckAvailabilityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckAvailabilityDocuments_TruckAvailabilities_TruckAvailabilityId",
                        column: x => x.TruckAvailabilityId,
                        principalTable: "TruckAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TruckAvailabilities_DriverId1",
                table: "TruckAvailabilities",
                column: "DriverId1");

            migrationBuilder.CreateIndex(
                name: "IX_TruckAvailabilities_MatchedLoadRequestId",
                table: "TruckAvailabilities",
                column: "MatchedLoadRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckAvailabilities_TruckId",
                table: "TruckAvailabilities",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckAvailabilityDocuments_TruckAvailabilityId",
                table: "TruckAvailabilityDocuments",
                column: "TruckAvailabilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckAvailabilityDocuments");

            migrationBuilder.DropTable(
                name: "TruckAvailabilities");
        }
    }
}