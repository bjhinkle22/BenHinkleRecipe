using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IUserFavoriteRepository : IDisposable
    {
        IEnumerable<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string userName, int recipeId);
        void DeleteFavoriteRecipe(string userName, int recipeId);
        void Save();
    }
}
