using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CookingBlog.Database;
using CookingBlog.Database.Models;
using NuGet.Protocol;

namespace CookingBlog.Web.Pages.Intranet.Admin.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly CookingBlog.Database.CookingBlogContext _ctx;

        public DeleteModel(CookingBlog.Database.CookingBlogContext context)
        {
            _ctx = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _ctx.Recipes.FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                Recipe = recipe;
            }
            return Page();
        }

        public IActionResult OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _ctx.Database.BeginTransaction();

            try
            {
                DeleteExistingData(id.Value);    

                _ctx.Recipes.Remove(new Recipe { Id = id.Value });
                _ctx.SaveChanges();

                _ctx.Database.CommitTransaction();
            }
            catch
            {
                _ctx.Database.RollbackTransaction();
            }

            return RedirectToPage("./Index");
        }

        private void DeleteExistingData(Guid recipeId)
        {
            var recipeGroupIds = _ctx
                .RecipeGroups
                .Where(g => g.RecipeId == recipeId)
                .Select(g => g.Id)
                .ToList();

            _ctx.RecipeGroupSteps
                        .RemoveRange(
                            _ctx.RecipeGroupSteps
                                .Where(r => recipeGroupIds.Contains(r.RecipeGroupId))
                                .ToList()
                        );

            _ctx.RecipeGroupIngredients
                .RemoveRange(
                    _ctx.RecipeGroupIngredients
                        .Where(i => recipeGroupIds.Contains(i.RecipeGroupId))
                );

            _ctx.RecipeToTags
                .RemoveRange(
                    _ctx.RecipeToTags
                        .Where(t => t.RecipeId == recipeId)
                );

            _ctx.RecipeNotes
                .RemoveRange(
                    _ctx.RecipeNotes
                        .Where(n => n.RecipeId == recipeId)
                );

            _ctx.RecipeStories
                .RemoveRange(
                    _ctx.RecipeStories
                        .Where(s => s.RecipeId == recipeId)
                );

            _ctx.RecipeGroups
                .RemoveRange(
                    _ctx.RecipeGroups
                        .Where(g => g.RecipeId == recipeId)
                );

            _ctx.SaveChanges();
        }
    }
}
