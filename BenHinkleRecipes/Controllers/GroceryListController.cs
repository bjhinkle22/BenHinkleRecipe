using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BenHinkleRecipes.Controllers
{
    public class GroceryListController : Controller
    {
        private readonly IGroceryListService _groceryListService;
        private readonly IGroceryListVMService _groceryListVMService;
        private readonly IIngredientService _ingredientService;

        public GroceryListController(IGroceryListService groceryListService, IGroceryListVMService groceryListVMService, IIngredientService ingredientService)
        {
            _groceryListService = groceryListService;
            _groceryListVMService = groceryListVMService;
            _ingredientService = ingredientService;
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

            foreach (var item in groceryIngredients)
            {
                item.userName = HttpContext.User.Identity.Name;
            }

            var needsUpdate = false;
            if(groceryList.Count > 0)
            {
                foreach(var item in groceryList)
                {
                    foreach(var ingredient in recipeIngredients)
                    {
                        for(var i = 0; i < groceryIngredients.Count; i++)
                        {
                            if (ingredient.Name == item.Name && ingredient.Name == groceryIngredients[i].Name)
                            {
                                item.Quantity = ingredient.Quantity + item.Quantity;
                                needsUpdate = true;
                                groceryIngredients.Remove(groceryIngredients[i]);
                            }
                        }
                    }
                    if (needsUpdate == true)
                    {
                        _groceryListService.UpdateGroceryList(groceryList);
                        needsUpdate = false;
                    }
                }
            }
            _groceryListService.InsertGroceryListItem(groceryIngredients);

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
