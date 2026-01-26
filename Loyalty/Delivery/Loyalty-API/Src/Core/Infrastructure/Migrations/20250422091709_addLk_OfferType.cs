using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLk_OfferType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LK_OfferTypeId",
                table: "OfferLikeUseds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LK_OfferType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_OfferType", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferLikeUseds_LK_OfferType_LK_OfferTypeId",
                table: "OfferLikeUseds");

            migrationBuilder.DropTable(
                name: "LK_OfferType");

            migrationBuilder.DropIndex(
                name: "IX_OfferLikeUseds_LK_OfferTypeId",
                table: "OfferLikeUseds");

            migrationBuilder.DropColumn(
                name: "LK_OfferTypeId",
                table: "OfferLikeUseds");
        }
    }
}
