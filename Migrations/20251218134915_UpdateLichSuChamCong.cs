using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLichSuChamCong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CaLam",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioRa",
                table: "LichSuChamCongs",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioVao",
                table: "LichSuChamCongs",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngay",
                table: "LichSuChamCongs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaLam",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "GioRa",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "GioVao",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "Ngay",
                table: "LichSuChamCongs");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "LichSuChamCongs");

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
