using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EletronicPartsCatalog.DataAccess.Migrations
{
    public partial class AddingJoinEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_Parts_PartId",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Objects_ObjectId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Projects_ProjectId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ObjectId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ProjectId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Objects_PartId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "Objects");

            migrationBuilder.CreateTable(
                name: "ObjectDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartDto_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObjectParts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    ObjectId1 = table.Column<int>(type: "int", nullable: true),
                    PartId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectParts", x => new { x.PartId, x.ObjectId });
                    table.ForeignKey(
                        name: "FK_ObjectParts_ObjectDto_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "ObjectDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectParts_Objects_ObjectId1",
                        column: x => x.ObjectId1,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObjectParts_PartDto_PartId",
                        column: x => x.PartId,
                        principalTable: "PartDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectParts_Parts_PartId1",
                        column: x => x.PartId1,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectParts_ObjectId",
                table: "ObjectParts",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectParts_ObjectId1",
                table: "ObjectParts",
                column: "ObjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectParts_PartId1",
                table: "ObjectParts",
                column: "PartId1");

            migrationBuilder.CreateIndex(
                name: "IX_PartDto_ProjectId",
                table: "PartDto",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectParts");

            migrationBuilder.DropTable(
                name: "ObjectDto");

            migrationBuilder.DropTable(
                name: "PartDto");

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "Objects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ObjectId",
                table: "Parts",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ProjectId",
                table: "Parts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_PartId",
                table: "Objects",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_Parts_PartId",
                table: "Objects",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Objects_ObjectId",
                table: "Parts",
                column: "ObjectId",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Projects_ProjectId",
                table: "Parts",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
