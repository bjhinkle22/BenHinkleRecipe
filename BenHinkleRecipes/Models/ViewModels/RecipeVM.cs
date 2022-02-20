using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenHinkleRecipes.Models.ViewModels
{
    [BindProperties]
    public class RecipeVM
    {
        [HiddenInput]
        public int RecipeId { get; set; }

        [Required]
        [DisplayName("Recipe Name")]
        public string? RecipeName { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }

        [DisplayName("Upload Front of Recipe Photo")]
        [Required]
        public IFormFile? RecipePhotoFrontFile { get; set; }

        [DisplayName("Upload Back of Recipe Photo")]
        [Required]
        public IFormFile? RecipePhotoBackFile { get; set; }
        public string? Description { get; set; }
        public bool? IsFavorite { get; set; }

        [Required]
        [DisplayName("Name of Recipe Front Photo")]
        public string? RecipePhotoFrontName { get; set; }

        [Required]
        [DisplayName("Name of Recipe Back Photo")]
        public string? RecipePhotoBackName { get; set; }

        public string? RecipeFrontDisplay { get; set; }

        public string? RecipeBackDisplay { get; set; }

        public byte[] OriginalRecipeFront { get; set; }
        public byte[] OriginalRecipeBack { get; set; }
    }
}
