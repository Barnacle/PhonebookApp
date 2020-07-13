using Microsoft.EntityFrameworkCore.Migrations;

namespace PhonebookAPI.Migrations
{
    public partial class AjustNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_Contacts_ContactId",
                table: "Numbers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Numbers",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<long>(
                name: "ContactId",
                table: "Numbers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Numbers_Contacts_ContactId",
                table: "Numbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numbers_Contacts_ContactId",
                table: "Numbers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Numbers",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<long>(
                name: "ContactId",
                table: "Numbers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Numbers_Contacts_ContactId",
                table: "Numbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
