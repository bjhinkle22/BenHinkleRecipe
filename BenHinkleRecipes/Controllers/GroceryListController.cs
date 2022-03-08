using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BenHinkleRecipes.Controllers
{
    public class GroceryListController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserRecipeService _userRecipeService;
        private readonly IGroceryListService _groceryListService;
        private readonly IGroceryListVMService _groceryListVMService;
        private readonly IIngredientService _ingredientService;
        private readonly IIngredientVMService _ingredientVMService;

        public GroceryListController(IGroceryListService groceryListService, IRecipeService recipeService, IGroceryListVMService groceryListVMService, IUserRecipeService userRecipeService, IIngredientService ingredientService, IIngredientVMService ingredientVMService)
        {
            _groceryListService = groceryListService;
            _recipeService = recipeService;
            _groceryListVMService = groceryListVMService;
            _userRecipeService = userRecipeService;
            _ingredientService = ingredientService;
            _ingredientVMService = ingredientVMService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGroceryList()
        {
            var groceryListItemsRMs = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListVMs = _groceryListVMService.RMListToVMList(groceryListItemsRMs);

            return View("GroceryList", groceryListVMs);
        }

        public IActionResult AddToGroceryList(int id)
        {

            var recipeIngredients = _ingredientService.GetRecipeIngredients(id).ToList();

            var groceryList = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryIngredients = _groceryListVMService.RecipeToGroceryListItem(recipeIngredients);

            var needsUpdate = false;
            if(groceryList.Count > 0)
            {
                foreach(var item in groceryList)
                {
                    foreach(var ingredient in recipeIngredients)
                    {
                        if(ingredient.Name == item.Name)
                        {
                            item.Quantity = ingredient.Quantity + item.Quantity;
                            needsUpdate = true;
                        }
                    }
                    if (needsUpdate == true)
                    {
                        _groceryListService.UpdateGroceryList(groceryList);
                    }
                }
            }
            if(needsUpdate == false)
            {
                _groceryListService.InsertGroceryListItem(groceryIngredients);
            }

            var groceryListReturn = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListDisplay = _groceryListVMService.RMListToVMList(groceryListReturn);

            return View("GroceryList", groceryListDisplay);

        }

        public IActionResult UserAddToGroceryList(int id)
        {
            //Get Recipe by id
            return View("GroceryList");
        }

        public IActionResult ClearGroceryList()
        {
            _groceryListService.ClearGroceryList(HttpContext.User.Identity.Name);

            List<GroceryListVM> groceryListVMs = new List<GroceryListVM>();
            return View("GroceryList", groceryListVMs);
        }
    }
}
