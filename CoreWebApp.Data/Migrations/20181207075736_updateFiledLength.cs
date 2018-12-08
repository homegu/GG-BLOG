using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebApp.Data.Migrations
{
    public partial class updateFiledLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "sys_User",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "sys_User",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
