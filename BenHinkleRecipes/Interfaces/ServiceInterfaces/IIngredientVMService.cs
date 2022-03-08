using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IIngredientVMService
    {
        List<GroceryListRepoModel> IngredientsToGroceryList(List<IngredientRepoModel> ingredientRepos);
        GroceryListRepoModel IRMToGRM(IngredientRepoModel ingredientRepoModel);
    }
}
