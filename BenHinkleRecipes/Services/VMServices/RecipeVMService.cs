﻿using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class RecipeVMService : IRecipeVMService
    {
        private readonly IRecipeService _recipeService;
        public RecipeVMService(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        public RecipeRepoModel VMtoRM(RecipeVM recipeVM)
        {
            RecipeRepoModel recipeRepoModel = new RecipeRepoModel();

                MemoryStream mStream = new MemoryStream();
                recipeVM.RecipePhotoFrontFile.CopyTo(mStream);
                var frontBytes = mStream.ToArray();

                MemoryStream mStream1 = new MemoryStream();
                recipeVM.RecipePhotoBackFile.CopyTo(mStream1);
                var backBytes = mStream1.ToArray();
                recipeRepoModel.RecipePhotoFront = frontBytes;
                recipeRepoModel.RecipePhotoBack = backBytes;
                recipeRepoModel.Id = recipeVM.RecipeId;
                recipeRepoModel.RecipeName = recipeVM.RecipeName;
                recipeRepoModel.Meat = recipeVM.Meat;
                recipeRepoModel.Veggies = recipeVM.Veggies;
                recipeRepoModel.Miscellaneous = recipeVM.Miscellaneous;
                recipeRepoModel.RecipePhotoFrontName = recipeVM.RecipePhotoFrontName;
                recipeRepoModel.RecipePhotoBackName = recipeVM.RecipePhotoBackName;
                recipeRepoModel.Description = recipeVM.Description;

                return recipeRepoModel;
        }
        public List<RecipeRepoModel> VMListToRMList(List<RecipeVM> recipeVMs)
        {
            List<RecipeRepoModel> recipeRepos = new List<RecipeRepoModel>();

            foreach (RecipeVM recipe in recipeVMs)
            {
                recipeRepos.Add(VMtoRM(recipe));
            }
            return recipeRepos;
        }
        public RecipeVM RMtoVM(RecipeRepoModel recipeRepoModel)
        {
            RecipeVM recipeVM = new RecipeVM();

            var base64Front = Convert.ToBase64String(recipeRepoModel.RecipePhotoFront);
            var base64Back = Convert.ToBase64String(recipeRepoModel.RecipePhotoBack);

            recipeVM.RecipeFrontDisplay = string.Format("data:image/jpg;base64,{0}", base64Front);
            recipeVM.RecipeBackDisplay = string.Format("data:image/jpg;base64,{0}", base64Back);
            recipeVM.RecipePhotoFrontName = recipeRepoModel.RecipePhotoFrontName;
            recipeVM.RecipePhotoBackName = recipeRepoModel.RecipePhotoBackName;
            recipeVM.RecipeId = recipeRepoModel.Id;
            recipeVM.RecipeName = recipeRepoModel.RecipeName;
            recipeVM.Meat = recipeRepoModel.Meat;
            recipeVM.Veggies = recipeRepoModel.Veggies;
            recipeVM.Miscellaneous = recipeRepoModel.Miscellaneous;
            recipeVM.Description = recipeRepoModel.Description;
            recipeVM.OriginalRecipeFront = recipeRepoModel.RecipePhotoFront;
            recipeVM.OriginalRecipeBack = recipeRepoModel.RecipePhotoBack;

            return recipeVM;
        }
        public List<RecipeVM> RMListToVMList(List<RecipeRepoModel> recipeRepos)
        {
            List<RecipeVM> recipeVMs = new List<RecipeVM>();

            foreach (RecipeRepoModel recipeRepo in recipeRepos)
            {
                recipeVMs.Add(RMtoVM(recipeRepo));
            }
            return recipeVMs;
        }
    }
}
