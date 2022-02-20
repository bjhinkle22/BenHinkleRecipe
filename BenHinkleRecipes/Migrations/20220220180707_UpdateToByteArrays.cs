using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenHinkleRecipes.Migrations
{
    public partial class UpdateToByteArrays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipePhotoFront",
                table: "Recipes",
                newName: "RecipePhotoFrontBytes");

            migrationBuilder.RenameColumn(
                name: "RecipePhotoBack",
                table: "Recipes",
                newName: "RecipePhotoBackBytes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipePhotoFrontBytes",
                table: "Recipes",
                newName: "RecipePhotoFront");

            migrationBuilder.RenameColumn(
                name: "RecipePhotoBackBytes",
                table: "Recipes",
                newName: "RecipePhotoBack");
        }
    }
}
