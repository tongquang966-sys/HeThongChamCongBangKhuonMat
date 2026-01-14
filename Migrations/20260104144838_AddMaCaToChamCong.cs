using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMaCaToChamCong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaLam",
                table: "LichSuChamCongs",
                newName: "TenCa");

            migrationBuilder.AddColumn<bool>(
                name: "DiTre",
                table: "LichSuChamCongs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaCa",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoPhutTre",
                table: "LichSuChamCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoPhutVeSom",
                table: "LichSuChamCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "VeSom",
                table: "LichSuChamCongs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiTre",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "MaCa",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "SoPhutTre",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "SoPhutVeSom",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "VeSom",
                table: "LichSuChamCongs");

            migrationBuilder.RenameColumn(
                name: "TenCa",
                table: "LichSuChamCongs",
                newName: "CaLam");
        }
    }
}
