using CookingBlog.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace CookingBlog.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController(CookingBlogContext ctx) : ControllerBase
    {
        private readonly CookingBlogContext _ctx = ctx;

        [HttpGet]
        [Route("{enumType}/{fileName}")]
        public IActionResult Get(string enumType, string fileName)
        {
            var media = _ctx
                .RecipeMedia
                .Where(r => r.FileName == fileName.ToLower())
                .FirstOrDefault();

            if (media == null)
            {
                return NotFound();
            }
            else
            {
                var ext = media.FileName.Split(".").Last();
                var type = "image/png";

                if (ext == "jpg" || ext == "jpeg")
                {
                    type = "image/jpeg";
                }

                return File(media.Data, type);
            }
        }
    }
}
