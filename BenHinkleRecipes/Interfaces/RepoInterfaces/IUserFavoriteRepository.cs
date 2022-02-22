using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IUserFavoriteRepository : IDisposable
    {
        IEnumerable<UserFavoriteRepoModel> GetFavoriteRecipes(string userName);
        void InsertFavoriteRecipe(string userName, int recipeId);
        void UpdateFavorite(int id, bool isFavorite, string username);
        void DeleteFavoriteRecipe(string userName, int recipeId);
        void Save();
    }
}
