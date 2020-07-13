using Microsoft.EntityFrameworkCore.Migrations;

namespace PhonebookAPI.Migrations
{
    public partial class TestUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Numbers_PhoneNumber",
                table: "Numbers",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Numbers_PhoneNumber",
                table: "Numbers");
        }
    }
}
