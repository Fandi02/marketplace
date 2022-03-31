using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace.Datas.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id_admin = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_admin);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "alamat",
                columns: table => new
                {
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_pembeli = table.Column<int>(type: "int(11)", nullable: false),
                    provinsi = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kota = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kecamatan = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    desa = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kode_pos = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_alamat);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "kategori",
                columns: table => new
                {
                    id_kategori = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_kategori);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pembeli",
                columns: table => new
                {
                    id_pembeli = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    foto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_admin = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pembeli);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "produk",
                columns: table => new
                {
                    id_produk = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    harga = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    stok = table.Column<int>(type: "int(4)", nullable: false),
                    gambar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_produk);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "status_order",
                columns: table => new
                {
                    id_status_order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_status_order);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "keranjang",
                columns: table => new
                {
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_pembeli = table.Column<int>(type: "int(11)", nullable: false),
                    jml_barang = table.Column<int>(type: "int(11)", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(25,2)", precision: 25, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_keranjang);
                    table.ForeignKey(
                        name: "keranjang_ibfk_1",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id_produk");
                    table.ForeignKey(
                        name: "keranjang_ibfk_2",
                        column: x => x.id_pembeli,
                        principalTable: "pembeli",
                        principalColumn: "id_pembeli");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "produk_kategori",
                columns: table => new
                {
                    id_produk_kategori = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_kategori = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_produk_kategori);
                    table.ForeignKey(
                        name: "produk_kategori_ibfk_1",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id_produk");
                    table.ForeignKey(
                        name: "produk_kategori_ibfk_2",
                        column: x => x.id_kategori,
                        principalTable: "kategori",
                        principalColumn: "id_kategori");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false),
                    tgl_transaksi = table.Column<DateOnly>(type: "date", nullable: false),
                    jml_bayar = table.Column<decimal>(type: "decimal(25)", precision: 25, nullable: false),
                    id_pembeli = table.Column<int>(type: "int(11)", nullable: false),
                    id_status_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    notes = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_order);
                    table.ForeignKey(
                        name: "order_ibfk_1",
                        column: x => x.id_keranjang,
                        principalTable: "keranjang",
                        principalColumn: "id_keranjang");
                    table.ForeignKey(
                        name: "order_ibfk_2",
                        column: x => x.id_pembeli,
                        principalTable: "pembeli",
                        principalColumn: "id_pembeli");
                    table.ForeignKey(
                        name: "order_ibfk_3",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id_alamat");
                    table.ForeignKey(
                        name: "order_ibfk_4",
                        column: x => x.id_status_order,
                        principalTable: "status_order",
                        principalColumn: "id_status_order");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pembayaran",
                columns: table => new
                {
                    id_pembayaran = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metode_pembayaran = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_pembeli = table.Column<int>(type: "int(11)", nullable: false),
                    jml_bayar = table.Column<decimal>(type: "decimal(25)", precision: 25, nullable: false),
                    tgl_bayar = table.Column<DateOnly>(type: "date", nullable: false),
                    pajak = table.Column<decimal>(type: "decimal(25)", precision: 25, nullable: false),
                    tujuan = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pembayaran);
                    table.ForeignKey(
                        name: "pembayaran_ibfk_1",
                        column: x => x.id_pembeli,
                        principalTable: "pembeli",
                        principalColumn: "id_pembeli");
                    table.ForeignKey(
                        name: "pembayaran_ibfk_2",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pengiriman",
                columns: table => new
                {
                    id_pengiriman = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    kurir = table.Column<int>(type: "int(11)", nullable: false),
                    ongkir = table.Column<int>(type: "int(11)", nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pengiriman);
                    table.ForeignKey(
                        name: "pengiriman_ibfk_1",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                    table.ForeignKey(
                        name: "pengiriman_ibfk_2",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id_alamat");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "id_pembeli",
                table: "alamat",
                column: "id_pembeli");

            migrationBuilder.CreateIndex(
                name: "id_pembeli1",
                table: "keranjang",
                column: "id_pembeli");

            migrationBuilder.CreateIndex(
                name: "id_produk",
                table: "keranjang",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "id_alamat",
                table: "order",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "id_keranjang",
                table: "order",
                column: "id_keranjang");

            migrationBuilder.CreateIndex(
                name: "id_pembeli2",
                table: "order",
                column: "id_pembeli");

            migrationBuilder.CreateIndex(
                name: "id_status_order",
                table: "order",
                column: "id_status_order");

            migrationBuilder.CreateIndex(
                name: "id_order",
                table: "pembayaran",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "id_pembeli3",
                table: "pembayaran",
                column: "id_pembeli");

            migrationBuilder.CreateIndex(
                name: "id_alamat1",
                table: "pengiriman",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "id_order1",
                table: "pengiriman",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "id_kategori",
                table: "produk_kategori",
                column: "id_kategori");

            migrationBuilder.CreateIndex(
                name: "id_produk1",
                table: "produk_kategori",
                column: "id_produk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "pembayaran");

            migrationBuilder.DropTable(
                name: "pengiriman");

            migrationBuilder.DropTable(
                name: "produk_kategori");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "kategori");

            migrationBuilder.DropTable(
                name: "keranjang");

            migrationBuilder.DropTable(
                name: "alamat");

            migrationBuilder.DropTable(
                name: "status_order");

            migrationBuilder.DropTable(
                name: "produk");

            migrationBuilder.DropTable(
                name: "pembeli");
        }
    }
}
