using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models;
using BenHinkleRecipes.Models.ViewModels;
using BenHinkleRecipes.Services.VMServices;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Identity;

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
            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if(userName == null)
            {
                var anonymousRecipes = _recipeService.GetRecipes();
                var anonymousRecipesResponse = _recipeVMService.RMListToVMList(anonymousRecipes);
                return View("Recipes", anonymousRecipesResponse);
            }

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();


            //Get All Recipes
            var allRecipes = _recipeService.GetRecipes();

            //Convert Repo Model to ViewModel
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

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
            var recipeRequest = _recipeService.GetRecipe(id);
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

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
            return View("_RecipeDetails", recipeVM);
        }

        [HttpGet]
        public ActionResult<RecipeVM> UpdateRecipe(int id)
        {
            var recipeRequest = _recipeService.GetRecipe(id);
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

            return View("_RecipeEdit", recipeResult);
        }

        [HttpPost]
        public ActionResult<RecipeVM> UpdateRecipe(RecipeVM request)
        {
            var recipeRequest = _recipeVMService.VMtoRM(request);
            _recipeService.UpdateRecipe(recipeRequest);
            var updatedRecipe = _recipeService.GetRecipe(request.RecipeId);
            var recipeResult = _recipeVMService.RMtoVM(updatedRecipe);

            return View("_RecipeDetails", recipeResult);

        }
        [HttpPost]
        public ActionResult<RecipeVM> DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
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

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}