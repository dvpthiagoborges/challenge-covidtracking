using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxTI.Challenge.CovidTracking.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InfoCovid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ActiveCases = table.Column<int>(type: "int(15)", nullable: true),
                    Country = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NewCases = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewDeaths = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalCases = table.Column<int>(type: "int(15)", nullable: false),
                    TotalDeaths = table.Column<int>(type: "int(15)", nullable: false),
                    TotalRecovered = table.Column<int>(type: "int(15)", nullable: false),
                    Active = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoCovid", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoCovid");
        }
    }
}
