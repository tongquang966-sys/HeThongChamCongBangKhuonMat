using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNhanVienDashboardFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DaChamCongHienTai",
                table: "NhanViens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaNV",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SoLanDiTre",
                table: "NhanViens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLanDungGio",
                table: "NhanViens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaChamCongHienTai",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "MaNV",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "SoLanDiTre",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "SoLanDungGio",
                table: "NhanViens");
        }
    }
}
