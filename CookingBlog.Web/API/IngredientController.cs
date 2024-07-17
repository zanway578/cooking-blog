using CookingBlog.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Usda.Fdc.Api;
using Usda.Fdc.Api.Models;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
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
            try
            {
                var apiClient = new FdcClient(_client);

                var results = await apiClient.SearchFoods(criteria);

                return results;
            }         
            catch (Exception ex) {
                var temp = "";
                throw ex;
            }
        }

        [HttpGet]
        [Route("nutrients/{fdcId}")]
        public async Task<IEnumerable<AbridgedFoodNutrient>> GetNutrients(int fdcId)
        {
            var apiClient = new FdcClient(_client);


            try
            {
                var results = await apiClient.GetAbridgedFood(fdcId);

                var filteredResults = results
                    .FoodNutrients
                    .Where(r => r.Amount > 0)
                    .OrderBy(r => r.Name)
                    .ToList();

                var exists = filteredResults
                    .Where(r => r.UnitName == "KCAL" && r.Name == "Energy")
                    .FirstOrDefault();

                if (exists == null)
                {
                    filteredResults.Add(new AbridgedFoodNutrient
                    {
                        Number = "19512",
                        Name = "Energy",
                        Amount = 1,
                        UnitName = "kCal"
                    });
                }
                else if (exists.Amount == 0)
                {
                    exists.Amount = 1;
                }

                return filteredResults;
            }
            catch (Exception ex)
            {
                var temp = "";
                throw ex;
            }
            
        }
    }
}
