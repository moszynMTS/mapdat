﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MapDat.AuthorizationServer.Data.Migrations.IdentityServer.UsersDb
{
    public partial class AddsFirstnameLastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");
        }
    }
}
