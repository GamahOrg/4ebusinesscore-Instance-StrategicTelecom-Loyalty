using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ServiceProviderLikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ServiceProviderLikes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ServiceProviderLikes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ServiceProviderLikes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "OfferLikeUseds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OfferLikeUseds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "OfferLikeUseds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "OfferLikeUseds",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ServiceProviderLikes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ServiceProviderLikes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ServiceProviderLikes");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ServiceProviderLikes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OfferLikeUseds");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OfferLikeUseds");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OfferLikeUseds");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "OfferLikeUseds");
        }
    }
}
