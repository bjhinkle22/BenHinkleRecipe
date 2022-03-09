﻿using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IIngredientVMService
    {
        IngredientVM RMtoVM(IngredientRepoModel ingredientRepoModel);
        IngredientRepoModel VMtoRM(IngredientVM ingredientVM);
    }
}
