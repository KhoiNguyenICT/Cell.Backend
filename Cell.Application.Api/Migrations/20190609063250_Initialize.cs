using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SETTING_FIELD",
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
                    ALLOW_FILTER = table.Column<int>(nullable: false),
                    ALLOW_SUMMARY = table.Column<int>(nullable: false),
                    CAPTION = table.Column<string>(maxLength: 200, nullable: true),
                    DATA_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    ORDINAL_POSITION = table.Column<int>(nullable: false),
                    PLACE_HOLDER = table.Column<string>(maxLength: 200, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    STORAGE_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_FIELD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SETTING_TABLE",
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
                    BASED_TABLE = table.Column<string>(maxLength: 200, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_TABLE", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SETTING_FIELD");

            migrationBuilder.DropTable(
                name: "T_SETTING_TABLE");
        }
    }
}
