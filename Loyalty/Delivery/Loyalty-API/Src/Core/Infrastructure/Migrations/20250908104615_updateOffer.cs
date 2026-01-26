using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Lk_DiscountMethods_Lk_DiscountMethodId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Lk_DiscountTypes_Lk_DiscountTypeId",
                table: "Offers");

            migrationBuilder.AlterColumn<Guid>(
                name: "Lk_DiscountTypeId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Lk_DiscountMethodId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Lk_DiscountMethods_Lk_DiscountMethodId",
                table: "Offers",
                column: "Lk_DiscountMethodId",
                principalTable: "Lk_DiscountMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Lk_DiscountTypes_Lk_DiscountTypeId",
                table: "Offers",
                column: "Lk_DiscountTypeId",
                principalTable: "Lk_DiscountTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Lk_DiscountMethods_Lk_DiscountMethodId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Lk_DiscountTypes_Lk_DiscountTypeId",
                table: "Offers");

            migrationBuilder.AlterColumn<Guid>(
                name: "Lk_DiscountTypeId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Lk_DiscountMethodId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Lk_DiscountMethods_Lk_DiscountMethodId",
                table: "Offers",
                column: "Lk_DiscountMethodId",
                principalTable: "Lk_DiscountMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Lk_DiscountTypes_Lk_DiscountTypeId",
                table: "Offers",
                column: "Lk_DiscountTypeId",
                principalTable: "Lk_DiscountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
