using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IRecipeRepository : IDisposable
    {
        IEnumerable<RecipeRepoModel> GetRecipes();
        RecipeRepoModel GetRecipe(int id);
        void InsertRecipe(RecipeRepoModel recipe);
        void DeleteRecipe(int id);
        void UpdateRecipe(RecipeRepoModel recipe);
        void Save();
    }
}
