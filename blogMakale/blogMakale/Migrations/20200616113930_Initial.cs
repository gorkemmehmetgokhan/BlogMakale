using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blogMakale.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiket",
                columns: table => new
                {
                    id_Etiket = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtiketAd = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiket", x => x.id_Etiket);
                });

            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    id_Kategori = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.id_Kategori);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    id_Kullanici = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.id_Kullanici);
                });

            migrationBuilder.CreateTable(
                name: "Makale",
                columns: table => new
                {
                    id_Makale = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Kullanici = table.Column<int>(nullable: false),
                    MakaleBaslik = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MakaleIcerik = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    MakaleTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makale", x => x.id_Makale);
                    table.ForeignKey(
                        name: "FK_Makale_Kullanici_id_Kullanici",
                        column: x => x.id_Kullanici,
                        principalTable: "Kullanici",
                        principalColumn: "id_Kullanici",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favori",
                columns: table => new
                {
                    id_Kullanici = table.Column<int>(nullable: false),
                    id_Makale = table.Column<int>(nullable: false),
                    FavoriTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favori", x => new { x.id_Makale, x.id_Kullanici });
                    table.ForeignKey(
                        name: "FK_Favori_Kullanici_id_Kullanici",
                        column: x => x.id_Kullanici,
                        principalTable: "Kullanici",
                        principalColumn: "id_Kullanici",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favori_Makale_id_Makale",
                        column: x => x.id_Makale,
                        principalTable: "Makale",
                        principalColumn: "id_Makale",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MakaleEtiket",
                columns: table => new
                {
                    id_Makale = table.Column<int>(nullable: false),
                    id_Etiket = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleEtiket", x => new { x.id_Makale, x.id_Etiket });
                    table.ForeignKey(
                        name: "FK_MakaleEtiket_Etiket_id_Etiket",
                        column: x => x.id_Etiket,
                        principalTable: "Etiket",
                        principalColumn: "id_Etiket",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakaleEtiket_Makale_id_Makale",
                        column: x => x.id_Makale,
                        principalTable: "Makale",
                        principalColumn: "id_Makale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakaleFoto",
                columns: table => new
                {
                    id_MakaleFoto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Makale = table.Column<int>(nullable: false),
                    FotoURL = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleFoto", x => x.id_MakaleFoto);
                    table.ForeignKey(
                        name: "FK_MakaleFoto_Makale_id_Makale",
                        column: x => x.id_Makale,
                        principalTable: "Makale",
                        principalColumn: "id_Makale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakaleKategori",
                columns: table => new
                {
                    id_Makale = table.Column<int>(nullable: false),
                    id_Kategori = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleKategori", x => new { x.id_Makale, x.id_Kategori });
                    table.ForeignKey(
                        name: "FK_MakaleKategori_Kategori_id_Kategori",
                        column: x => x.id_Kategori,
                        principalTable: "Kategori",
                        principalColumn: "id_Kategori",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakaleKategori_Makale_id_Makale",
                        column: x => x.id_Makale,
                        principalTable: "Makale",
                        principalColumn: "id_Makale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakaleYorum",
                columns: table => new
                {
                    id_MakaleYorum = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Makale = table.Column<int>(nullable: false),
                    id_Kullanici = table.Column<int>(nullable: false),
                    YorumText = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    YorumTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleYorum", x => x.id_MakaleYorum);
                    table.ForeignKey(
                        name: "FK_MakaleYorum_Kullanici_id_Kullanici",
                        column: x => x.id_Kullanici,
                        principalTable: "Kullanici",
                        principalColumn: "id_Kullanici",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MakaleYorum_Makale_id_Makale",
                        column: x => x.id_Makale,
                        principalTable: "Makale",
                        principalColumn: "id_Makale",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favori_id_Kullanici",
                table: "Favori",
                column: "id_Kullanici");

            migrationBuilder.CreateIndex(
                name: "IX_Makale_id_Kullanici",
                table: "Makale",
                column: "id_Kullanici");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleEtiket_id_Etiket",
                table: "MakaleEtiket",
                column: "id_Etiket");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleFoto_id_Makale",
                table: "MakaleFoto",
                column: "id_Makale");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleKategori_id_Kategori",
                table: "MakaleKategori",
                column: "id_Kategori");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleYorum_id_Kullanici",
                table: "MakaleYorum",
                column: "id_Kullanici");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleYorum_id_Makale",
                table: "MakaleYorum",
                column: "id_Makale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favori");

            migrationBuilder.DropTable(
                name: "MakaleEtiket");

            migrationBuilder.DropTable(
                name: "MakaleFoto");

            migrationBuilder.DropTable(
                name: "MakaleKategori");

            migrationBuilder.DropTable(
                name: "MakaleYorum");

            migrationBuilder.DropTable(
                name: "Etiket");

            migrationBuilder.DropTable(
                name: "Kategori");

            migrationBuilder.DropTable(
                name: "Makale");

            migrationBuilder.DropTable(
                name: "Kullanici");
        }
    }
}
