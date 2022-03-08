using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class RecipeVMService : IRecipeVMService
    {
        public RecipeRepoModel VMtoRM(RecipeVM recipeVM)
        {
            //Create new repo model to hold Repo values
            RecipeRepoModel recipeRepoModel = new();

            //Check null, if null retain original image
            if (recipeVM.RecipePhotoFrontFile == null)
            {
                recipeRepoModel.RecipePhotoFront = recipeVM.OriginalRecipeFront;
            }
            if (recipeVM.RecipePhotoBackFile == null)
            {
                recipeRepoModel.RecipePhotoBack = recipeVM.OriginalRecipeBack;
            }
            //If file is present in the VM, convert it to byte array, and assign it to the Repo Model
            if (recipeVM.RecipePhotoFrontFile != null)
            {
                recipeRepoModel.RecipePhotoFront = FileToByteArray(recipeVM.RecipePhotoFrontFile);
            }
            if (recipeVM.RecipePhotoBackFile != null)
            {
                recipeRepoModel.RecipePhotoBack = FileToByteArray(recipeVM.RecipePhotoBackFile);
            }

            //assign vm values to RM
            recipeRepoModel.Id = recipeVM.RecipeId;
            recipeRepoModel.RecipeName = recipeVM.RecipeName;
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
            //Create new VM to take values from the Repo Model
            RecipeVM recipeVM = new();

            //Check if there is a image stored in the Repo
            if (recipeRepoModel.RecipePhotoFront != null)
            {
                //Convert Byte Array image into Base 64 string
                var base64Front = Convert.ToBase64String(recipeRepoModel.RecipePhotoFront);

                //Assign base 64 string to the Display String in the VM, Format it
                recipeVM.RecipeFrontDisplay = string.Format("data:image/jpg;base64,{0}", base64Front);

                //Retain original byte array, in case user posts form without image (this way user doesn't have to upload the photo on every edit)
                recipeVM.OriginalRecipeFront = recipeRepoModel.RecipePhotoFront;
            }
            //Check if there is a image stored in the Repo
            if (recipeRepoModel.RecipePhotoBack != null)
            {
                //Convert Byte Array image into Base 64 string
                var base64Back = Convert.ToBase64String(recipeRepoModel.RecipePhotoBack);

                //Assign base 64 string to the Display String in the VM, Format it
                recipeVM.RecipeBackDisplay = string.Format("data:image/jpg;base64,{0}", base64Back);

                //Retain original byte array, in case user posts form without image (this way user doesn't have to upload the photo on every edit)
                recipeVM.OriginalRecipeBack = recipeRepoModel.RecipePhotoBack;
            }


            //Assign values from the Repo Model to the VM for displaying
            recipeVM.RecipeId = recipeRepoModel.Id;
            recipeVM.RecipeName = recipeRepoModel.RecipeName;
            recipeVM.Description = recipeRepoModel.Description;

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

        public byte[] FileToByteArray(IFormFile fileName)
        {
            MemoryStream mStream = new();
            fileName.CopyTo(mStream);
            var bytes = mStream.ToArray();
            return bytes;
        }
    }
}
