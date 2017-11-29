using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EletronicPartsCatalog.DataAccess.Migrations
{
    public partial class ChangingCreateByColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_AspNetUsers_CreatedById",
                table: "Objects");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_AspNetUsers_CreatedById",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_CreatedById",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Objects_CreatedById",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Objects");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Objects");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Objects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_CreatedById",
                table: "Parts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_CreatedById",
                table: "Objects",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_AspNetUsers_CreatedById",
                table: "Objects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_AspNetUsers_CreatedById",
                table: "Parts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
