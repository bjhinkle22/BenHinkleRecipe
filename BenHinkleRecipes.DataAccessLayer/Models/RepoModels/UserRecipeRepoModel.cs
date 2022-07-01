using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.DataAccessLayer.Models.RepoModels
{
    public class UserRecipeRepoModel
    {
        [Key]
        public int UserRecipe_ID { get; set; }
        public int recipe_id { get; set; }
        public string? RecipeName { get; set; }
        public string? Description { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoFront { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoBack { get; set; }
        public string userName { get; set; }
    }
}
