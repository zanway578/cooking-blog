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
using CookingBlog.Web.Lib;

namespace CookingBlog.Web.Pages.Intranet.Admin.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly CookingBlog.Database.CookingBlogContext _ctx;

        public string StatusMessage { get; set; } =  string.Empty;

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
            var formData = IngredientFormViewComponent.BuildFormModel(Request.Form);

            var ingredientSaver = new IngredientSaver(null, formData, _ctx);

            try
            {
                ingredientSaver.SaveIngredient();
            }
            catch (Exception ex) {
                StatusMessage = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}
