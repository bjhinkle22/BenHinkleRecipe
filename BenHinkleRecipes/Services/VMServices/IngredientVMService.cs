using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class IngredientVMService : IIngredientVMService
    {
        public IngredientVM RMtoVM(IngredientRepoModel ingredientRepoModel)
        {
            IngredientVM ingredientVM = new();

            ingredientVM.Name = ingredientRepoModel.Name;
            ingredientVM.Unit = ingredientRepoModel.Unit;
            ingredientVM.Quantity = ingredientRepoModel.Quantity;
            ingredientVM.recipe_id = ingredientRepoModel.recipe_id;
            ingredientVM.Category = ingredientRepoModel.Category;
            ingredientVM.userName = ingredientRepoModel.userName;

            return ingredientVM;
        }

        public IngredientRepoModel VMtoRM(IngredientVM ingredientVM)
        {
            IngredientRepoModel ingredientRepoModel = new();

            ingredientRepoModel.Name = ingredientVM.Name;
            ingredientRepoModel.Unit = ingredientVM.Unit;
            ingredientRepoModel.Quantity = ingredientVM.Quantity;
            ingredientRepoModel.recipe_id = ingredientVM.recipe_id;
            ingredientRepoModel.Category = ingredientVM.Category;
            ingredientRepoModel.userName = ingredientVM.userName;

            return ingredientRepoModel;
        }
        public List<IngredientVM> RMListToVMList(List<IngredientRepoModel> ingredientRepos)
        {
            List<IngredientVM> ingredientVMs = new();

            foreach (IngredientRepoModel ingredientRepo in ingredientRepos)
            {
                ingredientVMs.Add(RMtoVM(ingredientRepo));
            }
            return ingredientVMs;
        }
    }
}
