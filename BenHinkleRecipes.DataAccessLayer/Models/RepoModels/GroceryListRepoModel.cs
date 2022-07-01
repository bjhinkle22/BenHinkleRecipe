using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.DataAccessLayer.Models.RepoModels
{
    public class GroceryListRepoModel
    {
        [Key]
        public int? UserGroceryListID { get; set; }
        public int recipe_id { get; set; }
        public string userName { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
    }
}
