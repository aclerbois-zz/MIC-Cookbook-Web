using Mic.CookBook.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Mic.CookBook.Web.Database
{
    public class CookBookContext : DbContext
    {
        public CookBookContext(DbContextOptions<CookBookContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}