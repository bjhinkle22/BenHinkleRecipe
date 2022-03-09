using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id);
        void InsertIngredient(IngredientRepoModel ingredientRepo);
    }
}
