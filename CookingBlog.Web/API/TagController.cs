using CookingBlog.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController(CookingBlogContext ctx) : ControllerBase
    {
        private CookingBlogContext _ctx = ctx;

        [HttpGet]
        [Route("search")]
        public List<string> Search([FromQuery] string searchText)
        {
            var tags = _ctx
                .Tags
                .Where(t => t.Name.Contains(searchText))
                .Take(10)
                .Select(t => t.Name)
                .ToList();

            return tags;
        }

    }
}
