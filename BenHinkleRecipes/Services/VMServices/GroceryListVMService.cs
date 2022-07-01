using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class GroceryListVMService : IGroceryListVMService
    {
        public List<GroceryListRepoModel> RecipeToGroceryListItem(List<IngredientRepoModel> ingredientRepoModels)
        {
            
            List<GroceryListRepoModel> groceryListRepoModels = new List<GroceryListRepoModel>();
            foreach (var ing in ingredientRepoModels)
            {
                GroceryListRepoModel g = new GroceryListRepoModel();
                g.Name = ing.Name;
                g.Category = ing.Category;
                g.Unit = ing.Unit; 
                g.Quantity = ing.Quantity;
                g.recipe_id = ing.recipe_id;
                groceryListRepoModels.Add(g);
            }
            return groceryListRepoModels;
        }
        public GroceryListVM RMtoVM(GroceryListRepoModel groceryListRepoModel)
        {
            //Create new VM to take values from the Repo Model
            GroceryListVM groceryListVM = new();

            //Assign values from the Repo Model to the VM for displaying
            groceryListVM.recipe_id = groceryListRepoModel.recipe_id;
            groceryListVM.Quantity = groceryListRepoModel.Quantity;
            groceryListVM.Unit = groceryListRepoModel.Unit;
            groceryListVM.Name = groceryListRepoModel.Name;
            groceryListVM.Category = groceryListRepoModel.Category;

            return groceryListVM;
        }
        public List<GroceryListVM> RMListToVMList(List<GroceryListRepoModel> groceryListRepos)
        {
            List<GroceryListVM> groceryListVMs = new();

            foreach (GroceryListRepoModel groceryListRepo in groceryListRepos)
            {
                groceryListVMs.Add(RMtoVM(groceryListRepo));
            }
            return groceryListVMs;
        }

        public GroceryListRepoModel UserRecipeToGroceryListItem(UserRecipeRepoModel userRecipeRepoModel)
        {
            GroceryListRepoModel groceryListRepoModel = new GroceryListRepoModel();

            groceryListRepoModel.recipe_id = userRecipeRepoModel.recipe_id;

            return groceryListRepoModel;
        }
    }
}
