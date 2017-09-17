using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CCM.Data.Migrations
{
    public partial class addedappconfigandsponsor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationInfo",
                table: "Camps",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Id = table.Column<bool>(nullable: false, defaultValue: false),
                    CampName = table.Column<string>(nullable: true),
                    NextCamp = table.Column<int>(nullable: false),
                    Pic1 = table.Column<byte[]>(nullable: true),
                    Pic1ContentType = table.Column<string>(nullable: true),
                    Pic1FileName = table.Column<string>(nullable: true),
                    Pic2 = table.Column<byte[]>(nullable: true),
                    Pic2ContentType = table.Column<string>(nullable: true),
                    Pic2FileName = table.Column<string>(nullable: true),
                    Pic3 = table.Column<byte[]>(nullable: true),
                    Pic3ContentType = table.Column<string>(nullable: true),
                    Pic3FileName = table.Column<string>(nullable: true),
                    Pic4 = table.Column<byte[]>(nullable: true),
                    Pic4ContentType = table.Column<string>(nullable: true),
                    Pic4FileName = table.Column<string>(nullable: true),
                    Pic5 = table.Column<byte[]>(nullable: true),
                    Pic5ContentType = table.Column<string>(nullable: true),
                    Pic5FileName = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SponsorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CampId = table.Column<int>(nullable: false),
                    CompanyUrl = table.Column<string>(nullable: true),
                    Logo = table.Column<byte[]>(nullable: true),
                    LogoContentType = table.Column<string>(nullable: true),
                    LogoFileName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SponsorTypeId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sponsors_Camps_CampId",
                        column: x => x.CampId,
                        principalTable: "Camps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sponsors_SponsorTypes_SponsorTypeId",
                        column: x => x.SponsorTypeId,
                        principalTable: "SponsorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_CampId",
                table: "Sponsors",
                column: "CampId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_SponsorTypeId",
                table: "Sponsors",
                column: "SponsorTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "SponsorTypes");

            migrationBuilder.DropColumn(
                name: "LocationInfo",
                table: "Camps");
        }
    }
}
