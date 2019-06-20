using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class AddParentTextForSettingFieldInstance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PARENT_TEXT",
                table: "T_SETTING_FIELD_INSTANCE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PARENT_TEXT",
                table: "T_SETTING_FIELD_INSTANCE");
        }
    }
}
