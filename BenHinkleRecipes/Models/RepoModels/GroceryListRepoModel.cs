using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.RepoModels
{
    public class GroceryListRepoModel
    {
        [Key]
        public int UserGroceryListID { get; set; }
        public int recipe_id { get; set; }
        public string userName { get; set; }
    }
}
