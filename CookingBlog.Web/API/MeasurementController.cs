using CookingBlog.Database;
using CookingBlog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MeasurementController(CookingBlogContext ctx) : ControllerBase
    {
        private CookingBlogContext _ctx = ctx;

        [HttpGet]
        public List<DropdownListItem> Get()
        {
            return _ctx
                .Measurements
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new DropdownListItem()
                {
                    Name = $"{m.Name} ({m.UnitName})",
                    Value = m.Id.ToString()
                })
                .ToList();
        }
    }
}
