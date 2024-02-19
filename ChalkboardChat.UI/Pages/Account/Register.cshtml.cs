using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public string? Username { get; set; }
        public string? Password { get; set; }

        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            ApplicationUser newUser = new()
            {
                UserName = Username

            };

            var createUserResult = await _userManager.CreateAsync(newUser, Password);


            //_usersRepo.Add(newUser);

            if (createUserResult.Succeeded)
            {
                // Lyckats skapa en User
                // Logga in!

                ApplicationUser? userToLogin = await _userManager.FindByNameAsync(Username);

                var signInResult = await _signInManager.PasswordSignInAsync(userToLogin, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Index");
                }
                else
                {
                    // Fel lösenord!
                }
            }
            return Page();
        }
    }
}
