using System.ComponentModel.DataAnnotations;

namespace Mic.CookBook.Web.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Details { get; set; }

        public string Image { get; set; }

        public string Category { get; set; }

        public long Population { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}