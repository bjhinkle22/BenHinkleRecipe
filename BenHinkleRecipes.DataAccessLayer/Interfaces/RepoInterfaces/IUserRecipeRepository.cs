using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces
{
    public interface IUserRecipeRepository : IDisposable
    {
        IEnumerable<UserRecipeRepoModel> GetUserRecipes(string userName);
        UserRecipeRepoModel GetRecipe(int id);
        void InsertUserRecipe(UserRecipeRepoModel userRecipe);
        void DeleteUserRecipe(int id);
        void UpdateUserRecipe(UserRecipeRepoModel userRecipe);
        void Save();
    }
}
