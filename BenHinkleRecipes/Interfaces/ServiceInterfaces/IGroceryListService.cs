using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IGroceryListService
    {
        IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName);
        void InsertGroceryListItem(GroceryListRepoModel groceryListItem);
        void ClearGroceryList(string userName);
    }
}
