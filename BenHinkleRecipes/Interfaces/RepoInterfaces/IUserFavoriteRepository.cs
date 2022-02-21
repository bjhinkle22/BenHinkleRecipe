using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IUserFavoriteRepository : IDisposable
    {
        IEnumerable<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string currentUserId, int recipeId);
        void DeleteFavoriteRecipe(string currentUserId, int recipeId);
        void UpdateRecipe(string currentUserId, int recipeId);
        void Save();
    }
}
