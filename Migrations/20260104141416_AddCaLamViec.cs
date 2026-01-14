using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCaLamViec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaLamViecs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    SoGioCong = table.Column<double>(type: "float", nullable: false),
                    ChoPhepTrePhut = table.Column<int>(type: "int", nullable: false),
                    ChoPhepSomPhut = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaLamViecs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuChamCongs_NhanViens_NhanVienId",
                table: "LichSuChamCongs");

            migrationBuilder.DropTable(
                name: "CaLamViecs");

            migrationBuilder.DropIndex(
                name: "IX_LichSuChamCongs_NhanVienId",
                table: "LichSuChamCongs");
        }
    }
}
