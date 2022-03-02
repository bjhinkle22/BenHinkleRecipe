using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IRecipeService
    {
        List<RecipeRepoModel> GetRecipes();
        RecipeRepoModel GetRecipe(int id);
        RecipeRepoModel InsertRecipe(RecipeRepoModel recipe);
        RecipeRepoModel UpdateRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);

    }
}
