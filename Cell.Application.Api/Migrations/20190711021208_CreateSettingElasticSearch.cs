using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class CreateSettingElasticSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SETTING_ELASTIC_SEARCH",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OBJECT_ID = table.Column<Guid>(nullable: false),
                    TABLE_NAME = table.Column<string>(nullable: true),
                    TABLE_ID = table.Column<Guid>(nullable: false),
                    DATA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SETTING_ELASTIC_SEARCH", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SETTING_ELASTIC_SEARCH");
        }
    }
}
