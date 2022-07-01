using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IRecipeRepository : IDisposable
    {
        Task<IEnumerable<RecipeRepoModel>> GetRecipesAsync();
        RecipeRepoModel GetRecipe(int id);
        void InsertRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);
        void UpdateRecipe(RecipeRepoModel recipe);
        void Save();
    }
}