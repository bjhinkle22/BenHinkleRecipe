using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.RepoInterfaces
{
    public interface IGroceryListRepository
    {
        IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName);
        void InsertGroceryListItem(GroceryListRepoModel groceryListItem);
        void ClearGroceryList(string userName);
        void Save();
    }
}
