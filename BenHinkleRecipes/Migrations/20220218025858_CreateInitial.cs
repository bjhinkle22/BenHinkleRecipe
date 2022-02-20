using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenHinkleRecipes.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Veggies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Miscellaneous = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Favorite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipePhotoFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipePhotoBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
