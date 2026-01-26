using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeOtherToOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherMethod",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "OtherType",
                table: "Offers",
                newName: "Other");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Other",
                table: "Offers",
                newName: "OtherType");

            migrationBuilder.AddColumn<string>(
                name: "OtherMethod",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
