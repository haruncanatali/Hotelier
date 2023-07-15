using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelier.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Staff_Table_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Staffs");
        }
    }
}
