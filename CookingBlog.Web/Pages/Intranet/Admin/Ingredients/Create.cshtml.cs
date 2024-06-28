using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.ViewComponents;

namespace CookingBlog.Web.Pages.Intranet.Admin.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly CookingBlog.Database.CookingBlogContext _ctx;

        public CreateModel(CookingBlog.Database.CookingBlogContext context)
        {
            _ctx = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var formData = IngredientFormViewComponent.BuildFormModel(Request.Form, _ctx);

            throw new NotImplementedException();
        }
    }
}
