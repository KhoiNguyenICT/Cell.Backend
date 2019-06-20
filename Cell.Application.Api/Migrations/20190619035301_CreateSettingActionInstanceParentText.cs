using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class CreateSettingActionInstanceParentText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PARENT_TEXT",
                table: "T_SETTING_ACTION_INSTANCE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PARENT_TEXT",
                table: "T_SETTING_ACTION_INSTANCE");
        }
    }
}
