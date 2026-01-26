using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addArandEnConditons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Conditions",
                table: "Offers",
                newName: "ConditionsEn");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConditionsAr",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ConditionsAr",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ConditionsEn",
                table: "Offers",
                newName: "Conditions");
        }
    }
}
