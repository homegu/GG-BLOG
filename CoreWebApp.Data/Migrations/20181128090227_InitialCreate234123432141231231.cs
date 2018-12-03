using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebApp.Data.Migrations
{
    public partial class InitialCreate234123432141231231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "sys_User",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "sys_User",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "sys_User",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "sys_User",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32);
        }
    }
}
