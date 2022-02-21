using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IUserFavoriteService
    {
        List<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string userName, int recipeId);
        void DeleteFavoriteRecipe(string userName, int recipeId);
        void UpdateRecipe(string userName, int recipeId);
    }
}
