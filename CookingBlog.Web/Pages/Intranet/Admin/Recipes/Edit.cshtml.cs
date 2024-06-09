using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Models.Recipe;
using Microsoft.AspNetCore.Identity;
using CookingBlog.Web.Lib;
using CookingBlog.Web.ViewComponents;

namespace CookingBlog.Web.Pages.Intranet.Admin.Recipes
{
    public class EditModel(CookingBlogContext ctx, UserManager<IdentityApplicationUser> userManager) : PageModel
    {
        private readonly CookingBlogContext _ctx = ctx;
        private readonly UserManager<IdentityApplicationUser> _userManager = userManager;

        public string StatusMessage { get; set; }

        public Guid RecipeId { get; set; }

        public IActionResult OnGet()
        {
            RecipeId = new Guid(HttpContext.Request.Query["id"]);

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            RecipeId = new Guid(HttpContext.Request.Query["id"]);

            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid model state.";
                return Page();
            }

            try
            {
                var f = Request.Form.Files;

                var model = RecipeFormViewComponent.BuildFormModel(Request.Form, _ctx);

                new RecipeSaver(RecipeId, model, _ctx, _userManager, User).SaveRecipe();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
