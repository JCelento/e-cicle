using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Api.Migrations
{
    public partial class AddAuthorToComponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorPersonId",
                table: "Components",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_AuthorPersonId",
                table: "Components",
                column: "AuthorPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Persons_AuthorPersonId",
                table: "Components",
                column: "AuthorPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Persons_AuthorPersonId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_AuthorPersonId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "AuthorPersonId",
                table: "Components");
        }
    }
}
