using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.RepoModels
{
    public class GroceryListRepoModel
    {
        [Key]
        public int UserGroceryListID { get; set; }
        public int recipe_id { get; set; }
        public string? Meat { get; set; }
        public string? Veggies { get; set; }
        public string? Miscellaneous { get; set; }
        public string userName { get; set; }
    }
}
