using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCM.Data.Migrations
{
    public partial class ModifiedTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<bool>(nullable: false, defaultValue: false),
                    CampName = table.Column<string>(maxLength: 40, nullable: true),
                    NextCampId = table.Column<int>(nullable: true),
                    Pic1 = table.Column<byte[]>(nullable: true),
                    Pic1ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    Pic1FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic2 = table.Column<byte[]>(nullable: true),
                    Pic2ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    Pic2FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic3 = table.Column<byte[]>(nullable: true),
                    Pic3ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic3FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic4 = table.Column<byte[]>(nullable: true),
                    Pic4ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic4FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic5 = table.Column<byte[]>(nullable: true),
                    Pic5ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic5FileName = table.Column<string>(maxLength: 200, nullable: true),
                    TagLine = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Id = table.Column<bool>(nullable: false, defaultValue: false),
                    CampName = table.Column<string>(maxLength: 40, nullable: true),
                    NextCampId = table.Column<int>(nullable: true),
                    Pic1 = table.Column<byte[]>(nullable: true),
                    Pic1ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    Pic1FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic2 = table.Column<byte[]>(nullable: true),
                    Pic2ContentType = table.Column<string>(maxLength: 50, nullable: true),
                    Pic2FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic3 = table.Column<byte[]>(nullable: true),
                    Pic3ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic3FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic4 = table.Column<byte[]>(nullable: true),
                    Pic4ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic4FileName = table.Column<string>(maxLength: 200, nullable: true),
                    Pic5 = table.Column<byte[]>(nullable: true),
                    Pic5ContentType = table.Column<string>(maxLength: 40, nullable: true),
                    Pic5FileName = table.Column<string>(maxLength: 200, nullable: true),
                    TagLine = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Id);
                });
        }
    }
}
