using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class RemoveData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_VIEW");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_TABLE");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_REPORT");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_FORM");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_FILTER");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_FIELD_INSTANCE");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_FIELD");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_FEATURE");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_ADVANCED");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_ACTION_INSTANCE");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SETTING_ACTION");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SECURITY_USER");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SECURITY_SESSION");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SECURITY_PERMISSION");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "T_SECURITY_GROUP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_VIEW",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_TABLE",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_REPORT",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_FORM",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_FILTER",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_FIELD_INSTANCE",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_FIELD",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_FEATURE",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_ADVANCED",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_ACTION_INSTANCE",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SETTING_ACTION",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SECURITY_USER",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SECURITY_SESSION",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SECURITY_PERMISSION",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DATA",
                table: "T_SECURITY_GROUP",
                type: "xml",
                nullable: true);
        }
    }
}
