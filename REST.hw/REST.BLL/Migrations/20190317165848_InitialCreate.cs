using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace REST.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_persons",
                columns: table => new
                {
                    cln_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cln_name = table.Column<string>(nullable: true),
                    cln_surname = table.Column<string>(nullable: true),
                    cln_birth_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_persons", x => x.cln_id);
                });

            migrationBuilder.InsertData(
                table: "tbl_persons",
                columns: new[] { "cln_id", "cln_birth_date", "cln_name", "cln_surname" },
                values: new object[] { 1, new DateTime(2018, 2, 10, 19, 58, 48, 192, DateTimeKind.Local), "John", "Doe" });

            migrationBuilder.InsertData(
                table: "tbl_persons",
                columns: new[] { "cln_id", "cln_birth_date", "cln_name", "cln_surname" },
                values: new object[] { 2, new DateTime(2017, 1, 18, 19, 58, 48, 194, DateTimeKind.Local), "Tom", "Moto" });

            migrationBuilder.InsertData(
                table: "tbl_persons",
                columns: new[] { "cln_id", "cln_birth_date", "cln_name", "cln_surname" },
                values: new object[] { 3, new DateTime(2018, 2, 10, 19, 58, 48, 194, DateTimeKind.Local), "Fred", "Smitt" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_persons");
        }
    }
}
