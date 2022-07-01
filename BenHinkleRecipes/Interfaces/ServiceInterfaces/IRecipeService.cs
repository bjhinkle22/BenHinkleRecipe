using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IRecipeService
    {
        Task<List<RecipeRepoModel>> GetRecipesAsync();
        RecipeRepoModel GetRecipe(int id);
        RecipeRepoModel InsertRecipe(RecipeRepoModel recipe);
        RecipeRepoModel UpdateRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);
    }
}