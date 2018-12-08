using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebApp.Data.Migrations
{
    public partial class addFiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_User",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 32, nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Token = table.Column<string>(maxLength: 200, nullable: true),
                    PassWord = table.Column<string>(maxLength: 32, nullable: false),
                    LoginCount = table.Column<int>(nullable: false),
                    RegisterIP = table.Column<string>(maxLength: 50, nullable: false),
                    LastLoginIP = table.Column<string>(maxLength: 50, nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_User");
        }
    }
}
