using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mona_Logistics_LTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoadRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CargoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightKg = table.Column<double>(type: "float", nullable: false),
                    VolumeM3 = table.Column<double>(type: "float", nullable: false),
                    IsFragile = table.Column<bool>(type: "bit", nullable: false),
                    RequiresCooling = table.Column<bool>(type: "bit", nullable: false),
                    PickupAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredPickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreferredVehicleType = table.Column<int>(type: "int", nullable: false),
                    MinCargoSpaceM3 = table.Column<double>(type: "float", nullable: false),
                    MaxWeightCapacityKg = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadRequests", x => x.Id);
                    table.CheckConstraint("CK_LoadRequest_VolumeM3", "VolumeM3 > 0");
                    table.CheckConstraint("CK_LoadRequest_WeightKg", "WeightKg > 0");
                    table.ForeignKey(
                        name: "FK_LoadRequests_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoadRequestDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoadRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_LoadRequestDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoadRequestDocuments_LoadRequests_LoadRequestId",
                        column: x => x.LoadRequestId,
                        principalTable: "LoadRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoadRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.CheckConstraint("CK_Offer_Discount", "Discount >= 0 AND Discount <= Price");
                    table.CheckConstraint("CK_Offer_Price", "Price >= 0");
                    table.ForeignKey(
                        name: "FK_Offers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_LoadRequests_LoadRequestId",
                        column: x => x.LoadRequestId,
                        principalTable: "LoadRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadRequestDocuments_LoadRequestId",
                table: "LoadRequestDocuments",
                column: "LoadRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadRequests_ClientId",
                table: "LoadRequests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadRequests_PreferredPickupDate",
                table: "LoadRequests",
                column: "PreferredPickupDate");

            migrationBuilder.CreateIndex(
                name: "IX_LoadRequests_Status",
                table: "LoadRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CreatedById",
                table: "Offers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_LoadRequestId",
                table: "Offers",
                column: "LoadRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_Status",
                table: "Offers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ValidUntil",
                table: "Offers",
                column: "ValidUntil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadRequestDocuments");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "LoadRequests");
        }
    }
}
