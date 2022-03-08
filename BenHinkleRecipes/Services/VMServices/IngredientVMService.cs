using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class IngredientVMService : IIngredientVMService
    {
        public List<GroceryListRepoModel> IngredientsToGroceryList(List<IngredientRepoModel> ingredientRepos)
        {
            List<GroceryListRepoModel> groceryListRepoModels = new();

            foreach (IngredientRepoModel ingredientRepo in ingredientRepos)
            {
                groceryListRepoModels.Add(IRMToGRM(ingredientRepo));
            }

            return groceryListRepoModels;
        }

        public GroceryListRepoModel IRMToGRM(IngredientRepoModel ingredientRepoModel)
        {
            //Create new VM to take values from the Repo Model
            GroceryListRepoModel groceryListRepo = new();

            //Assign values from the Repo Model to the VM for displaying
            groceryListRepo.Name = ingredientRepoModel.Name;
            groceryListRepo.Category = ingredientRepoModel.Category;
            groceryListRepo.Quantity = ingredientRepoModel.Quantity;
            groceryListRepo.Unit = ingredientRepoModel.Unit;

            return groceryListRepo;
        }
    }
}
