using Microsoft.EntityFrameworkCore.Migrations;

namespace MapDat.AuthorizationServer.Data.Migrations.IdentityServer.UsersDb
{
    public partial class AddLpToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lp",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lp",
                table: "AspNetUsers");
        }
    }
}
