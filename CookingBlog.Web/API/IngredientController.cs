using CookingBlog.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController(CookingBlogContext ctx) : ControllerBase
    {
        private CookingBlogContext _ctx = ctx;

        [HttpGet]
        [Route("search")]
        public List<string> Search([FromQuery] string searchText) {
            var ingredients = _ctx
                .Ingredients
                .Where(i => i.Name.Contains(searchText))
                .Take(10)
                .Select(i => i.Name)
                .ToList();

            return ingredients;
        }

    }
}
