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
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }

        [Required]
        [DisplayName("Recipe Name")]
        public string? RecipeName { get; set; }
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
