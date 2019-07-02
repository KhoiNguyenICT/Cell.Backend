using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class CreateSettingFilterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SETTING_FILTER",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CODE = table.Column<string>(nullable: true),
                    NAME = table.Column<string>(nullable: true),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    DATA = table.Column<string>(type: "xml", nullable: true),
                    CREATED = table.Column<DateTimeOffset>(nullable: false),
                    CREATED_BY = table.Column<Guid>(nullable: false),
                    MODIFIED = table.Column<DateTimeOffset>(nullable: false),
                    MODIFIED_BY = table.Column<Guid>(nullable: false),
                    VERSION = table.Column<int>(nullable: false),
                    SETTINGS = table.Column<string>(nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_ID_TEXT = table.Column<string>(nullable: true),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_FILTER", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SETTING_FILTER");
        }
    }
}
