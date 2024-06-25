using CookingBlog.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Usda.Fdc.Api;
using Usda.Fdc.Api.Models;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class IngredientController(CookingBlogContext ctx, HttpClient client) : ControllerBase
    {
        private CookingBlogContext _ctx = ctx;
        private HttpClient _client = client;

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

        [HttpPost]
        [Route("search/nutrients")]
        public async Task<SearchResult> SearchIngredients(FoodSearchCriteria criteria)
        {
            var apiClient = new FdcClient(_client);

            var results = await apiClient.SearchFoods(criteria);

            return results;
        }

        [HttpGet]
        [Route("nutrients/{fdcId}")]
        public async Task<IEnumerable<AbridgedFoodNutrient>> GetNutrients(int fdcId)
        {
            var apiClient = new FdcClient(_client);

            var results = await apiClient.GetAbridgedFood(fdcId);

            return results.FoodNutrients; 
        }
    }
}
