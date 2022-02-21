using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IRecipeService
    {
        List<RecipeRepoModel> GetRecipes();
        List<RecipeRepoModel> GetFavoriteRecipes();
        RecipeRepoModel GetRecipe(int id);
        RecipeRepoModel InsertRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);
        void SetFavorite(int id, bool isFavorite);
        RecipeRepoModel UpdateRecipe(RecipeRepoModel recipe);
    }
}
