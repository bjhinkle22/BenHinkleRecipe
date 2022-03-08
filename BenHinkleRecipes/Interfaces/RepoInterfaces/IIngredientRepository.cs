using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id);

        IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName);
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName, int id);
    }
}
