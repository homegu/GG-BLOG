using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebApp.Data.Migrations
{
    public partial class InitialCreate23412343214 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "sys_User");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "sys_User");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "sys_User");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "sys_User");

            migrationBuilder.RenameColumn(
                name: "RegIP",
                table: "sys_User",
                newName: "RegisterIP");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "sys_User",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "sys_User",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterIP",
                table: "sys_User",
                newName: "RegIP");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "sys_User",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "sys_User",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "sys_User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "sys_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "sys_User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "sys_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
