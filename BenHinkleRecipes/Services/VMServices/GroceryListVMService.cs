﻿using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class GroceryListVMService : IGroceryListVMService
    {
        public GroceryListRepoModel RecipeToGroceryListItem(RecipeRepoModel recipeRepoModel)
        {

            GroceryListRepoModel groceryListRepoModel = new GroceryListRepoModel();

            groceryListRepoModel.recipe_id = recipeRepoModel.Id;

            return groceryListRepoModel;
        }
        public GroceryListVM RMtoVM(GroceryListRepoModel groceryListRepoModel)
        {
            //Create new VM to take values from the Repo Model
            GroceryListVM groceryListVM = new();

            //Assign values from the Repo Model to the VM for displaying
            groceryListVM.recipe_id = groceryListRepoModel.recipe_id;

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
