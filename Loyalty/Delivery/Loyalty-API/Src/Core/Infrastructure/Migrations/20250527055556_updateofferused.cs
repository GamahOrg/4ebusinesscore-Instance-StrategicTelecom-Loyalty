#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateofferused : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferLikeUseds_LK_OfferType_LK_OfferTypeId",
                table: "OfferLikeUseds");

            migrationBuilder.DropIndex(
                name: "IX_OfferLikeUseds_LK_OfferTypeId",
                table: "OfferLikeUseds");

            migrationBuilder.DropColumn(
                name: "LK_OfferTypeId",
                table: "OfferLikeUseds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LK_OfferTypeId",
                table: "OfferLikeUseds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfferLikeUseds_LK_OfferTypeId",
                table: "OfferLikeUseds",
                column: "LK_OfferTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferLikeUseds_LK_OfferType_LK_OfferTypeId",
                table: "OfferLikeUseds",
                column: "LK_OfferTypeId",
                principalTable: "LK_OfferType",
                principalColumn: "Id");
        }
    }
}
