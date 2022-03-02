using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IUserRecipeService
    {
        List<UserRecipeRepoModel> GetUserRecipes(string userName);
        UserRecipeRepoModel GetRecipe(int id);
        UserRecipeRepoModel InsertUserRecipe(UserRecipeRepoModel userRecipe);
        UserRecipeRepoModel UpdateUserRecipe(UserRecipeRepoModel userRecipe);
        void DeleteUserRecipe(int id);

    }
}
