using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SETTING_ACTION",
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
                    CONTAINER_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_ACTION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SETTING_ACTION_INSTANCE",
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
                    CONTAINER_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    ACTION_ID = table.Column<Guid>(nullable: false),
                    ORDINAL_POSITION = table.Column<int>(nullable: false),
                    PARENT = table.Column<Guid>(nullable: false),
                    PARENT_TEXT = table.Column<string>(nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_ACTION_INSTANCE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SETTING_ADVANCED",
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
                    INDEX_LEFT = table.Column<int>(nullable: false),
                    INDEX_RIGHT = table.Column<int>(nullable: false),
                    IS_LEAF = table.Column<int>(nullable: false),
                    NODE_LEVEL = table.Column<int>(nullable: false),
                    PARENT = table.Column<Guid>(nullable: true),
                    PATH_CODE = table.Column<string>(maxLength: 1000, nullable: true),
                    PATH_ID = table.Column<string>(maxLength: 1000, nullable: true),
                    SETTING_VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_ADVANCED", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SETTING_FEATURE",
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
                    ICON = table.Column<string>(maxLength: 50, nullable: true),
                    INDEX_LEFT = table.Column<int>(nullable: false),
                    INDEX_RIGHT = table.Column<int>(nullable: false),
                    IS_LEAF = table.Column<int>(nullable: false),
                    NODE_LEVEL = table.Column<int>(nullable: false),
                    PARENT = table.Column<Guid>(nullable: true),
                    PATH_CODE = table.Column<string>(maxLength: 1000, nullable: true),
                    PATH_ID = table.Column<string>(maxLength: 1000, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_FEATURE", x => x.ID);
                });

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
                name: "T_SETTING_FIELD_INSTANCE",
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
                    CAPTION = table.Column<string>(maxLength: 200, nullable: true),
                    CONTAINER_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    DATA_TYPE = table.Column<string>(maxLength: 50, nullable: true),
                    FIELD_ID = table.Column<Guid>(nullable: false),
                    ORDINAL_POSITION = table.Column<int>(nullable: false),
                    PARENT = table.Column<Guid>(nullable: false),
                    PARENT_TEXT = table.Column<string>(nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    STORAGE_TYPE = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_FIELD_INSTANCE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SETTING_FORM",
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
                    LAYOUT_ID = table.Column<Guid>(nullable: false),
                    SETTINGS = table.Column<string>(nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_FORM", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "T_SETTING_VIEW",
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
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_VIEW", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SETTING_ACTION");

            migrationBuilder.DropTable(
                name: "T_SETTING_ACTION_INSTANCE");

            migrationBuilder.DropTable(
                name: "T_SETTING_ADVANCED");

            migrationBuilder.DropTable(
                name: "T_SETTING_FEATURE");

            migrationBuilder.DropTable(
                name: "T_SETTING_FIELD");

            migrationBuilder.DropTable(
                name: "T_SETTING_FIELD_INSTANCE");

            migrationBuilder.DropTable(
                name: "T_SETTING_FORM");

            migrationBuilder.DropTable(
                name: "T_SETTING_TABLE");

            migrationBuilder.DropTable(
                name: "T_SETTING_VIEW");
        }
    }
}
