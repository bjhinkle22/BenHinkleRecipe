using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IUserFavoriteService
    {
        List<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string currentUserId, int recipeId);
        void DeleteFavoriteRecipe(string currentUserId, int recipeId);
        void UpdateRecipe(string currentUserId, int recipeId);
    }
}
