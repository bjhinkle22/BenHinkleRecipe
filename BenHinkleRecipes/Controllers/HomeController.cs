using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models;
using BenHinkleRecipes.Models.ViewModels;
using BenHinkleRecipes.Services.VMServices;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System;

namespace BenHinkleRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _recipeService;
        private readonly IRecipeVMService _recipeVMService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService, IRecipeVMService recipeVMService, IWebHostEnvironment hostEnvironment)
        {
            _recipeVMService = recipeVMService;
            _hostEnvironment = hostEnvironment;
            _recipeService = recipeService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var recipeRequest = _recipeService.GetRecipes();
            var recipeResponse = _recipeVMService.RMListToVMList(recipeRequest);
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
            var recipeResponse = _recipeService.UpdateRecipe(recipeRequest);
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
        public ActionResult<RecipeVM> GetFavoriteRecipes(int id)
        {
            var recipeRequest = _recipeService.GetRecipes();
            var recipeResponse = _recipeVMService.RMListToVMList(recipeRequest);
            return View("Recipes", recipeResponse);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}