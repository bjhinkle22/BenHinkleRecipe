using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IIngredientService
    {
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id);
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName);
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName, int id);
    }
}
