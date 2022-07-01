using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IUserFavoriteService
    {
        List<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string userName, int recipeId);
        void UpdateFavorite(int id, bool isFavorite, string username);
        void DeleteFavoriteRecipe(string userName, int recipeId);
    }
}
