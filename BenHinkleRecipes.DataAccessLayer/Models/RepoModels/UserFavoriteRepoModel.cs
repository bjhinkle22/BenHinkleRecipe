using System.ComponentModel.DataAnnotations;

namespace BenHinkleRecipes.DataAccessLayer.Models.RepoModels
{
    public class UserFavoriteRepoModel
    {
        [Key]
        public int UserFavorite_ID { get; set; }
        public int recipe_id { get; set; }
        public string userName { get; set; }
    }
}
