namespace BenHinkleRecipes.Models.RepoModels
{
    public class RecipeRepoModel
    {
        public int Id { get; set; }
        public string? RecipeName { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }
        public bool? IsFavorite { get; set; }
        public string? Description { get; set; }
        public string? RecipePhotoFrontName { get; set; }
        public string? RecipePhotoBackName { get; set; }
        public byte[]? RecipePhotoFront { get; set; }
        public byte[]? RecipePhotoBack { get; set; }
    }
}
