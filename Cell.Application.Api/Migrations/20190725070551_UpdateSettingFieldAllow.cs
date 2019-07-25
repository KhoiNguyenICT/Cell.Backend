using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class UpdateSettingFieldAllow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ALLOW_SUMMARY",
                table: "T_SETTING_FIELD",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "ALLOW_FILTER",
                table: "T_SETTING_FIELD",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "INDEX_LEFT",
                table: "T_SETTING_FEATURE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "INDEX_RIGHT",
                table: "T_SETTING_FEATURE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IS_LEAF",
                table: "T_SETTING_FEATURE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NODE_LEVEL",
                table: "T_SETTING_FEATURE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PARENT",
                table: "T_SETTING_FEATURE",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PATH_CODE",
                table: "T_SETTING_FEATURE",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PATH_ID",
                table: "T_SETTING_FEATURE",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "INDEX_LEFT",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "INDEX_RIGHT",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "IS_LEAF",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "NODE_LEVEL",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "PARENT",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "PATH_CODE",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "PATH_ID",
                table: "T_SETTING_FEATURE");

            migrationBuilder.AlterColumn<int>(
                name: "ALLOW_SUMMARY",
                table: "T_SETTING_FIELD",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "ALLOW_FILTER",
                table: "T_SETTING_FIELD",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
