using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.ViewModels
{
    public class GroceryListVM
    {
        [Key]
        public int UserGroceryListID { get; set; }
        public int recipe_id { get; set; }
        public string userName { get; set; }
    }
}
