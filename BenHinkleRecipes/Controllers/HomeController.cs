using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models;
using BenHinkleRecipes.Models.ViewModels;
using BenHinkleRecipes.Services.VMServices;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Identity;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeVMService _recipeVMService;
        IUserFavoriteService _userFavoriteService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IRecipeService recipeService, IRecipeVMService recipeVMService, UserManager<IdentityUser> userManager, IUserFavoriteService userFavoriteService)
        {
            _recipeVMService = recipeVMService;
            _recipeService = recipeService;
            _userManager = userManager;
            _userFavoriteService = userFavoriteService;
        }
        public IActionResult Index()
        {
            //Get All Recipes
            var allRecipes = _recipeService.GetRecipes();

            //Convert List of Repo Models to  List of ViewModels
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if(userName == null)
            {
                return View("Recipes", recipeResponse);
            }

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //do work to assign favorite to user's favorite recipe
            foreach (RecipeVM recipe in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    int test = favorites[i];
                    if (recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                    }
                }
            }
            return View("Recipes", recipeResponse);
        }

        [HttpGet]
        public ActionResult<RecipeVM> GetRecipeById(int id)
        {
            //Get Recipe by Id
            var recipeRequest = _recipeService.GetRecipe(id);

            //Convert Repo Model to View Model
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
                return View("_RecipeDetails", recipeResult);
            }
            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //Adding IsFavorite to Appropriate Recipes
            for (int i = 0; i < favorites.Count; i++)
            {
                int test = favorites[i];
                if (recipeResult.RecipeId == favorites[i])
                {
                    recipeResult.IsFavorite = true;
                }
            }
            return View("_RecipeDetails", recipeResult);
        }

        [HttpGet]
        public ActionResult<RecipeVM> CreateRecipe()
        {
            return View("_RecipeCreate");
        }

        [HttpPost]
        public ActionResult<RecipeVM> CreateRecipe(RecipeVM request)
        {
          
            var recipeRequest = _recipeVMService.VMtoRM(request);
            var recipeResponse = _recipeService.InsertRecipe(recipeRequest);
            var recipeVM = _recipeVMService.RMtoVM(recipeResponse);

            //Check if user checked Favorite & UserName isn't null
            if (request.IsFavorite == true && HttpContext.User.Identity.Name != null)
            {
                //Call Add UserFavorite
                _userFavoriteService.InsertFavoriteRecipe(HttpContext.User.Identity.Name, recipeVM.RecipeId);
                recipeVM.IsFavorite = true;
            }

            return View("_RecipeDetails", recipeVM);
        }

        [HttpGet]
        public ActionResult<RecipeVM> UpdateRecipe(int id)
        {
            var recipeRequest = _recipeService.GetRecipe(id);
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
                return View("_RecipeEdit", recipeResult);
            }

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();


            //Adding IsFavorite to Appropriate Recipes
            for (int i = 0; i < favorites.Count; i++)
            {
                int test = favorites[i];
                if (recipeResult.RecipeId == favorites[i])
                {
                    recipeResult.IsFavorite = true;
                }
            }
            return View("_RecipeEdit", recipeResult);
        }

        [HttpPost]
        public ActionResult<RecipeVM> UpdateRecipe(RecipeVM request)
        {
            var recipeRequest = _recipeVMService.VMtoRM(request);
            _recipeService.UpdateRecipe(recipeRequest);
            var updatedRecipe = _recipeService.GetRecipe(request.RecipeId);
            var recipeResult = _recipeVMService.RMtoVM(updatedRecipe);

            if (request.IsFavorite == true && HttpContext.User.Identity.Name != null)
            {
                //Call Add UserFavorite
                _userFavoriteService.InsertFavoriteRecipe(HttpContext.User.Identity.Name, request.RecipeId);
                recipeResult.IsFavorite = true;
            }

            return View("_RecipeDetails", recipeResult);

        }

        [HttpPost]
        public ActionResult<RecipeVM> DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            if(HttpContext.User.Identity != null)
            {
                _userFavoriteService.DeleteFavoriteRecipe(HttpContext.User.Identity.Name, id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult<RecipeVM> GetFavoriteRecipes()
        {
            //Get current UserName
           var userName = HttpContext.User.Identity.Name;

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            var allRecipes = _recipeService.GetRecipes();
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

            var favoriteRecipes = new List<RecipeVM>();

            foreach (RecipeVM recipe in recipeResponse)
            {
               for(int i = 0; i < favorites.Count; i++)
                {
                    int test = favorites[i];
                    if(recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                        favoriteRecipes.Add(recipe);
                    }
                }
            }
            return View("Recipes", favoriteRecipes);
        }

        public void SetFavorite(int recipeId, bool check)
        {
            var userName = HttpContext.User.Identity.Name;
            _userFavoriteService.UpdateFavorite(recipeId, check, userName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}