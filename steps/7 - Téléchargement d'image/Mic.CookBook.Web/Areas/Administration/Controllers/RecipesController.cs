using System.Linq;
using System.Threading.Tasks;
using Mic.CookBook.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mic.CookBook.Web.Database;
using Mic.CookBook.Web.Helpers;
using Mic.CookBook.Web.Models;

namespace Mic.CookBook.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class RecipesController : Controller
    {
        private readonly CookBookContext _context;
        private readonly FileSaver _fileSaver;

        public RecipesController(CookBookContext context, FileSaver fileSaver)
        {
            _context = context;
            _fileSaver = fileSaver;
        }

        // GET: Administration/Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: Administration/Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Administration/Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Details,Image,Category,Population,Latitude,Longitude")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Administration/Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var editRecipeViewModel = new EditRecipeViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Category = recipe.Category,
                Details = recipe.Details,
                Image = recipe.Image,
                Latitude = recipe.Latitude,
                Location = recipe.Location,
                Longitude = recipe.Longitude,
                Population = recipe.Population
            };

            return View(editRecipeViewModel);
        }

        // POST: Administration/Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Details,Image,ImageFile,Category,Population,Latitude,Longitude")] EditRecipeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var recipe = new Recipe()
                    {
                        Id = model.Id,
                        Details = model.Details,
                        Name = model.Name,
                        Population = model.Population,
                        Category = model.Category,
                        Location = model.Location,
                        Image = model.Image,
                        Longitude = model.Longitude,
                        Latitude = model.Latitude,
                    };

                    if (model.ImageFile != null)
                    {
                        recipe.Image = await _fileSaver.SaveFileToWwwFolder($"{model.Id}.jpg", model.ImageFile);
                    }
                    
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Administration/Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Administration/Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
