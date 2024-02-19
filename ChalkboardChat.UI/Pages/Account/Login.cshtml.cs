using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPost()
        {
            ApplicationUser userToLogIn = await _userManager.FindByNameAsync(Username);

            if (userToLogIn != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Index");
                }
            }

            return Page();
        }
    }
}
