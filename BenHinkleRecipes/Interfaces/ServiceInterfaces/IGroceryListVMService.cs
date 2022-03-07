using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IGroceryListVMService
    {
        GroceryListVM RMtoVM(GroceryListRepoModel groceryListRepoModel);

        List<GroceryListVM> RMListToVMList(List<GroceryListRepoModel> groceryListRepos);
        GroceryListRepoModel RecipeToGroceryListItem(RecipeRepoModel recipeRepoModel);
    }
}
