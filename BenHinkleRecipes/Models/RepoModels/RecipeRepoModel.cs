using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.RepoModels
{
    public class RecipeRepoModel
    {
        public int Id { get; set; }
        public string? RecipeName { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }
        public string? Description { get; set; }
        public bool IsFavorite { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoFront { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoBack { get; set; }
    }
}
