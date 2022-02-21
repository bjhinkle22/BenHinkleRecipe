using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class RecipeVMService : IRecipeVMService
    {
        public RecipeRepoModel VMtoRM(RecipeVM recipeVM)
        {
            RecipeRepoModel recipeRepoModel = new();

            if (recipeVM.RecipePhotoFrontFile == null)
            {
                recipeRepoModel.RecipePhotoFront = recipeVM.OriginalRecipeFront;
            }
            if (recipeVM.RecipePhotoBackFile == null)
            {
                recipeRepoModel.RecipePhotoBack = recipeVM.OriginalRecipeBack;
            }
            if (recipeVM.RecipePhotoFrontFile != null)
            {
                MemoryStream mStream = new();
                recipeVM.RecipePhotoFrontFile.CopyTo(mStream);
                var frontBytes = mStream.ToArray();
                recipeRepoModel.RecipePhotoFront = frontBytes;
            }
            if (recipeVM.RecipePhotoBackFile != null)
            {
                MemoryStream mStream1 = new();
                recipeVM.RecipePhotoBackFile.CopyTo(mStream1);
                var backBytes = mStream1.ToArray();
                recipeRepoModel.RecipePhotoBack = backBytes;
            }
            recipeRepoModel.Id = recipeVM.RecipeId;
            recipeRepoModel.RecipeName = recipeVM.RecipeName;
            recipeRepoModel.Meat = recipeVM.Meat;
            recipeRepoModel.Veggies = recipeVM.Veggies;
            recipeRepoModel.Miscellaneous = recipeVM.Miscellaneous;
            recipeRepoModel.Description = recipeVM.Description;

            return recipeRepoModel;
        }
        public List<RecipeRepoModel> VMListToRMList(List<RecipeVM> recipeVMs)
        {
            List<RecipeRepoModel> recipeRepos = new();

            foreach (RecipeVM recipe in recipeVMs)
            {
                recipeRepos.Add(VMtoRM(recipe));
            }
            return recipeRepos;
        }
        public RecipeVM RMtoVM(RecipeRepoModel recipeRepoModel)
        {
            RecipeVM recipeVM = new();

            var base64Front = Convert.ToBase64String(recipeRepoModel.RecipePhotoFront);
            var base64Back = Convert.ToBase64String(recipeRepoModel.RecipePhotoBack);

            recipeVM.RecipeFrontDisplay = string.Format("data:image/jpg;base64,{0}", base64Front);
            recipeVM.RecipeBackDisplay = string.Format("data:image/jpg;base64,{0}", base64Back);
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
            List<RecipeVM> recipeVMs = new();

            foreach (RecipeRepoModel recipeRepo in recipeRepos)
            {
                recipeVMs.Add(RMtoVM(recipeRepo));
            }
            return recipeVMs;
        }
    }
}
