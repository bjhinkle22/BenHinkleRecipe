using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Services
{
    public class UserRecipeService : IUserRecipeService
    {
        private readonly IUserRecipeRepository _userRecipeRepo;

        public UserRecipeService(IUserRecipeRepository userRecipeRepo)
        {
            _userRecipeRepo = userRecipeRepo;
        }

        public List<UserRecipeRepoModel> GetUserRecipes(string userName)
        {
            List<UserRecipeRepoModel> userRecipes = _userRecipeRepo.GetUserRecipes(userName).ToList();
            return userRecipes;
        }
        public UserRecipeRepoModel GetRecipe(int id)
        {
            UserRecipeRepoModel recipe = _userRecipeRepo.GetRecipe(id);
            return recipe;
        }

        public UserRecipeRepoModel InsertUserRecipe(UserRecipeRepoModel userRecipe)
        {
            _userRecipeRepo.InsertUserRecipe(userRecipe);
            return userRecipe;
        }

        public UserRecipeRepoModel UpdateUserRecipe(UserRecipeRepoModel userRecipe)
        {
            _userRecipeRepo.UpdateUserRecipe(userRecipe);
            return userRecipe;
        }
        public void DeleteUserRecipe(int id)
        {
            _userRecipeRepo.DeleteUserRecipe(id);
        }
    }
}
