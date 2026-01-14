using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_CaLamViecId_And_ImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaLamViecId",
                table: "NhanViens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "NhanViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_CaLamViecId",
                table: "NhanViens",
                column: "CaLamViecId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanViens_CaLamViecs_CaLamViecId",
                table: "NhanViens",
                column: "CaLamViecId",
                principalTable: "CaLamViecs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanViens_CaLamViecs_CaLamViecId",
                table: "NhanViens");

            migrationBuilder.DropIndex(
                name: "IX_NhanViens_CaLamViecId",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "CaLamViecId",
                table: "NhanViens");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "NhanViens");
        }
    }
}
