using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.CookBook.Web.Database;
using Mic.CookBook.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mic.CookBook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly CookBookContext cookBookContext;

        public RecipesController(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Recipe>> Get()
        {
            var recipes = cookBookContext.Recipes.ToList();
            if (recipes.Count == 0)
                return NotFound();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(404)]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = cookBookContext.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }
    }
}