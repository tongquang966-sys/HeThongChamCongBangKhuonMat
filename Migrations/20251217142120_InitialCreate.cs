using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuChamCongs_NhanViens_NhanVienId",
                table: "LichSuChamCongs");

            migrationBuilder.DropIndex(
                name: "IX_LichSuChamCongs_NhanVienId",
                table: "LichSuChamCongs");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FaceEmbedding",
                table: "NhanViens",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AnhNhanVien",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChucVu",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "LichSuChamCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnhNhanVien",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "ChucVu",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "LichSuChamCongs");

            migrationBuilder.AlterColumn<string>(
                name: "FaceEmbedding",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuChamCongs_NhanVienId",
                table: "LichSuChamCongs",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuChamCongs_NhanViens_NhanVienId",
                table: "LichSuChamCongs",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
