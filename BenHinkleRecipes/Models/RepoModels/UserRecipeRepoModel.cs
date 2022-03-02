﻿using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.RepoModels
{
    public class UserRecipeRepoModel
    {
        [Key]
        public int UserRecipe_ID { get; set; }
        public int recipe_id { get; set; }
        public string? RecipeName { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }
        public string? Description { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoFront { get; set; }
        [MaxLength]
        public byte[]? RecipePhotoBack { get; set; }
        public string userName { get; set; }
    }
}
