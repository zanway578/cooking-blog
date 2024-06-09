using CookingBlog.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBlog.Web.Pages.Intranet
{
    public class IndexModel(SignInManager<IdentityApplicationUser> signinManager) : PageModel
    {
        private SignInManager<IdentityApplicationUser> _signInManager = signinManager;

        public IActionResult OnGet()
        {

            try
            {
                _signInManager.SignOutAsync();
            }
            catch
            {

            }

            return Redirect("/intranet/login");
        }
    }
}
