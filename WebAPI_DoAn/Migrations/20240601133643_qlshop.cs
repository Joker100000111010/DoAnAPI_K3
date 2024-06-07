using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_DoAn.Migrations
{
    /// <inheritdoc />
    public partial class qlshop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loaiSanPhams",
                columns: table => new
                {
                    IdLoaiSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgLSP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiSanPhams", x => x.IdLoaiSP);
                });

            migrationBuilder.CreateTable(
                name: "nhanViens",
                columns: table => new
                {
                    IdNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanViens", x => x.IdNV);
                });

            migrationBuilder.CreateTable(
                name: "shops",
                columns: table => new
                {
                    Idsp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTien = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTrendingProduct = table.Column<bool>(type: "bit", nullable: false),
                    loaiSanPhamIdLoaiSP = table.Column<int>(type: "int", nullable: true),
                    IdLoaiSP = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shops", x => x.Idsp);
                    table.ForeignKey(
                        name: "FK_shops_loaiSanPhams_loaiSanPhamIdLoaiSP",
                        column: x => x.loaiSanPhamIdLoaiSP,
                        principalTable: "loaiSanPhams",
                        principalColumn: "IdLoaiSP");
                });

            migrationBuilder.CreateTable(
                name: "nhanVienShops",
                columns: table => new
                {
                    NhanVienShopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shopIdsp = table.Column<int>(type: "int", nullable: true),
                    Idsp = table.Column<int>(type: "int", nullable: true),
                    NhanVienIdNV = table.Column<int>(type: "int", nullable: true),
                    IdNV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanVienShops", x => x.NhanVienShopId);
                    table.ForeignKey(
                        name: "FK_nhanVienShops_nhanViens_NhanVienIdNV",
                        column: x => x.NhanVienIdNV,
                        principalTable: "nhanViens",
                        principalColumn: "IdNV");
                    table.ForeignKey(
                        name: "FK_nhanVienShops_shops_shopIdsp",
                        column: x => x.shopIdsp,
                        principalTable: "shops",
                        principalColumn: "Idsp");
                });

            migrationBuilder.CreateIndex(
                name: "IX_nhanVienShops_NhanVienIdNV",
                table: "nhanVienShops",
                column: "NhanVienIdNV");

            migrationBuilder.CreateIndex(
                name: "IX_nhanVienShops_shopIdsp",
                table: "nhanVienShops",
                column: "shopIdsp");

            migrationBuilder.CreateIndex(
                name: "IX_shops_loaiSanPhamIdLoaiSP",
                table: "shops",
                column: "loaiSanPhamIdLoaiSP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nhanVienShops");

            migrationBuilder.DropTable(
                name: "nhanViens");

            migrationBuilder.DropTable(
                name: "shops");

            migrationBuilder.DropTable(
                name: "loaiSanPhams");
        }
    }
}
