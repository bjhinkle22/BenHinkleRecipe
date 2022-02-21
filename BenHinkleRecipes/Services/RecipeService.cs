using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepo;

        public RecipeService(IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }
        public List<RecipeRepoModel> GetRecipes()
        {
            List<RecipeRepoModel> recipes = _recipeRepo.GetRecipes().ToList();
            return recipes;
        }
        public List<RecipeRepoModel> GetFavoriteRecipes()
        {
            List<RecipeRepoModel> recipes = _recipeRepo.GetFavoriteRecipes().ToList();
            return recipes;
        }
        public RecipeRepoModel GetRecipe(int id)
        {
            RecipeRepoModel recipe = _recipeRepo.GetRecipe(id);
            return recipe;
        }
        public RecipeRepoModel InsertRecipe(RecipeRepoModel recipe)
        {
            _recipeRepo.InsertRecipe(recipe);
            return recipe;
        }
        public RecipeRepoModel UpdateRecipe(RecipeRepoModel recipe)
        {
            _recipeRepo.UpdateRecipe(recipe);
            return recipe;
        }
        public void DeleteRecipe(int id)
        {
            _recipeRepo.DeleteRecipe(id);
        }
    }
}
