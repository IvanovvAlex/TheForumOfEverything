using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheForumOfEverything.Migrations
{
    public partial class updateee1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagsToString",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsToString",
                table: "Posts");
        }
    }
}
