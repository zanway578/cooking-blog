using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CookingBlog.Database;
using CookingBlog.Database.Models;
using CookingBlog.Web.Models.RecipeData;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;
using CookingBlog.Web.Lib;
using CookingBlog.Web.ViewComponents;

namespace CookingBlog.Web.Pages.Intranet.Admin.Recipes
{
    public class CreateModel(CookingBlogContext ctx, UserManager<IdentityApplicationUser> userManager) : PageModel
    {
        private readonly CookingBlogContext _ctx = ctx;
        private readonly UserManager<IdentityApplicationUser> _userManager = userManager;

        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid model state.";
                return Page();
            }

            try
            {
                var model = RecipeFormViewComponent.BuildFormModel(Request.Form, _ctx);

                new RecipeSaver(null, model, _ctx, _userManager, User).SaveRecipe();
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
