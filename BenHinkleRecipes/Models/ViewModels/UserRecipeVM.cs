using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenHinkleRecipes.Models.ViewModels
{
    public class UserRecipeVM
    {
        [HiddenInput]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserRecipe_ID { get; set; }
        public int RecipeId { get; set; }
        [Required]
        [DisplayName("Recipe Name")]
        public string? RecipeName { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }
        [DisplayName("Upload Front of Recipe Photo")]
        public IFormFile? RecipePhotoFrontFile { get; set; }
        [DisplayName("Upload Back of Recipe Photo")]
        public IFormFile? RecipePhotoBackFile { get; set; }
        public string? Description { get; set; }
        public bool IsFavorite { get; set; }
        public string? RecipeFrontDisplay { get; set; }
        public string? RecipeBackDisplay { get; set; }
        [HiddenInput]
        public byte[]? OriginalRecipeFront { get; set; }
        [HiddenInput]
        public byte[]? OriginalRecipeBack { get; set; }
   
    }
}
