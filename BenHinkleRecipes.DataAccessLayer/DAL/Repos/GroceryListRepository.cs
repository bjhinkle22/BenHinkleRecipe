using BenHinkleRecipes.DataAccessLayer.Context;
using BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces;
using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DataAccessLayer.Repos
{
    public class GroceryListRepository : IGroceryListRepository, IDisposable
    {

        private readonly DataContext _context;

        public GroceryListRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName)
        {
            return _context.GroceryLists.Where(u => u.userName == userName);
        }
        public void InsertGroceryListItem(List<GroceryListRepoModel> groceryListItems)
        {
            foreach(var item in groceryListItems)
            {
                _context.GroceryLists.Add(item);
                Save();
            }
        }
        public void UpdateGroceryList(List<GroceryListRepoModel> groceryListItems)
        {
            foreach( var item in groceryListItems)
            {
                _context.Entry(item).State = EntityState.Modified;
                Save();
            }
        }
        public void ClearGroceryList(string userName)
        {
            List<GroceryListRepoModel> groceryListItems = _context.GroceryLists.Where(u => u.userName == userName).ToList();
            foreach(GroceryListRepoModel groceryListRepo in groceryListItems)
            {
                _context.GroceryLists.Remove(groceryListRepo);
                Save();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }
}
