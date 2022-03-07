using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.Models.ViewModels
{
    public class GroceryListVM
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
