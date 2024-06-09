using CookingBlog.Database;
using CookingBlog.Database.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBlog.Web.Pages.Intranet
{
    public class LoginModel(SignInManager<IdentityApplicationUser> signinManager, CookingBlogContext ctx) : PageModel
    {
        public string LoginMessage { get; set; } = "";

        private SignInManager<IdentityApplicationUser> _signinManager = signinManager;
        private CookingBlogContext _ctx = ctx;

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/intranet/admin/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogin(string username, string password)
        {

            try
            {

                var result = await _signinManager.PasswordSignInAsync(username, password, false, false);


                if (result.Succeeded)
                {
                    return Redirect("/intranet/admin");
                }
                else
                {
                    LoginMessage = "Login failed";
                }
            }
            catch (Exception ex)
            {
                LoginMessage = "Login error";
            }


            return Page();
        }
    }
}
