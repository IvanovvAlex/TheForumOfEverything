using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheForumOfEverything.Migrations
{
    public partial class Update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Tags",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Tags",
                newName: "Text");
        }
    }
}
