using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mona_Logistics_LTD.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixedCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Offer_Discount",
                table: "Offers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Offer_Price",
                table: "Offers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests");

            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_WeightKg",
                table: "LoadRequests");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offer_Discount",
                table: "Offers",
                sql: "\"Discount\" >= 0 AND \"Discount\" <= \"Price\"");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offer_Price",
                table: "Offers",
                sql: "\"Price\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests",
                sql: "\"LinearMeters\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_WeightKg",
                table: "LoadRequests",
                sql: "\"WeightKg\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Offer_Discount",
                table: "Offers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Offer_Price",
                table: "Offers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests");

            migrationBuilder.DropCheckConstraint(
                name: "CK_LoadRequest_WeightKg",
                table: "LoadRequests");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offer_Discount",
                table: "Offers",
                sql: "Discount >= 0 AND Discount <= Price");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offer_Price",
                table: "Offers",
                sql: "Price >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_LinearMeters",
                table: "LoadRequests",
                sql: "LinearMeters > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_LoadRequest_WeightKg",
                table: "LoadRequests",
                sql: "WeightKg > 0");
        }
    }
}
