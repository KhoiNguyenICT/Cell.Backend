using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class CreateGroupUserPermissionAndSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SECURITY_GROUP",
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
                    PARENT = table.Column<Guid>(nullable: false),
                    PATH_CODE = table.Column<string>(maxLength: 1000, nullable: true),
                    PATH_ID = table.Column<string>(maxLength: 1000, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    STATUS = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SECURITY_GROUP", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SECURITY_PERMISSION",
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
                    AUTHORIZED_ID = table.Column<Guid>(nullable: false),
                    AUTHORIZED_TYPE = table.Column<string>(maxLength: 200, nullable: true),
                    OBJECT_ID = table.Column<Guid>(nullable: false),
                    OBJECT_NAME = table.Column<string>(maxLength: 200, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    TABLE_NAME = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SECURITY_PERMISSION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SECURITY_SESSION",
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
                    EXPIRED_TIME = table.Column<int>(nullable: false),
                    SETTINGS = table.Column<string>(nullable: true),
                    SIGNIN_TIME = table.Column<DateTimeOffset>(nullable: false),
                    USER_ID = table.Column<Guid>(nullable: false),
                    USER_ACCOUNT = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SECURITY_SESSION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SECURITY_USER",
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
                    ACCOUNT = table.Column<string>(maxLength: 200, nullable: true),
                    DEFAULT_DEPARTMENT = table.Column<Guid>(nullable: false),
                    DEFAULT_ROLE = table.Column<Guid>(nullable: false),
                    EMAIL = table.Column<string>(maxLength: 200, nullable: true),
                    ENCRYPTED_PASSWORD = table.Column<string>(maxLength: 1000, nullable: true),
                    PHONE = table.Column<string>(maxLength: 50, nullable: true),
                    SETTINGS = table.Column<string>(nullable: true),
                    STATUS = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SECURITY_USER", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SECURITY_GROUP");

            migrationBuilder.DropTable(
                name: "T_SECURITY_PERMISSION");

            migrationBuilder.DropTable(
                name: "T_SECURITY_SESSION");

            migrationBuilder.DropTable(
                name: "T_SECURITY_USER");
        }
    }
}
