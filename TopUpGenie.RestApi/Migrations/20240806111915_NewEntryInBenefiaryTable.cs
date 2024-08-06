using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopUpGenie.RestApi.Migrations
{
    /// <inheritdoc />
    public partial class NewEntryInBenefiaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Beneficiaries",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Beneficiaries");
        }
    }
}
