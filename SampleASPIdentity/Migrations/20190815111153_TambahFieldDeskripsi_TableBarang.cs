using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleASPIdentity.Migrations
{
    public partial class TambahFieldDeskripsi_TableBarang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deskripsi",
                table: "Barang",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deskripsi",
                table: "Barang");
        }
    }
}
