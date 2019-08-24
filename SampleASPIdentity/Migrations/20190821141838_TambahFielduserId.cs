using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleASPIdentity.Migrations
{
    public partial class TambahFielduserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "streams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "streams");
        }
    }
}
