using System.Linq;
using Mic.CookBook.Web.Database;
using Microsoft.AspNetCore.Mvc;

namespace Mic.CookBook.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly CookBookContext cookBookContext;

        public RecipeController(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }

        public IActionResult Index()
        {
            var list = cookBookContext.Recipes.ToList();
            return View(list);
        }

        public IActionResult Details(int id, string name = null)
        {
            var recipe = cookBookContext.Recipes.FirstOrDefault(r => r.Id == id);
            return View(recipe);
        }
    }
}