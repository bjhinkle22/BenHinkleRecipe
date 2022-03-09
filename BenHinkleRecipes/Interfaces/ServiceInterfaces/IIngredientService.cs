using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IIngredientService
    {
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id);
        void InsertIngredient(IngredientRepoModel ingredientRepo);
    }
}
