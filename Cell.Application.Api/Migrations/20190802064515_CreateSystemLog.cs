using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cell.Application.Api.Migrations
{
    public partial class CreateSystemLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SYSTEM_LOG",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APPLICATION = table.Column<string>(nullable: true),
                    LOGGED = table.Column<string>(nullable: true),
                    LEVEL = table.Column<string>(nullable: true),
                    MESSAGE = table.Column<string>(nullable: true),
                    LOGGER = table.Column<string>(nullable: true),
                    CALL_SITE = table.Column<string>(nullable: true),
                    EXCEPTION = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SYSTEM_LOG", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SYSTEM_LOG");
        }
    }
}
