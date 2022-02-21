using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IRecipeService
    {
        List<RecipeRepoModel> GetRecipes();
        RecipeRepoModel GetRecipe(int id);
        RecipeRepoModel InsertRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);
        RecipeRepoModel UpdateRecipe(RecipeRepoModel recipe);
    }
}
