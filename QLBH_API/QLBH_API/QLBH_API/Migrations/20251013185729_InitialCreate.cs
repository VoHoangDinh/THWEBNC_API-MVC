using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBH_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbDANHMUC",
                columns: table => new
                {
                    MADANHMUC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENDANHMUC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DANHMUCCHA = table.Column<int>(type: "int", nullable: true),
                    MOTA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbDANHMUC", x => x.MADANHMUC);
                });

            migrationBuilder.CreateTable(
                name: "tbGIOHANG",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAKHACHHANG = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MASANPHAM = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbGIOHANG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbSANPHAM",
                columns: table => new
                {
                    MASANPHAM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENSANPHAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DONGIA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SOLUONG = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HINHANH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOTA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MADANHMUC = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSANPHAM", x => x.MASANPHAM);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbDANHMUC");

            migrationBuilder.DropTable(
                name: "tbGIOHANG");

            migrationBuilder.DropTable(
                name: "tbSANPHAM");
        }
    }
}
