using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Member
{
    [BindProperties]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _authDbContext;
        public ApplicationUser SignedInUser { get; set; }
        public string NewUsername { get; set; }

        public EditUserModel(UserManager<ApplicationUser> userManager, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _authDbContext = authDbContext;
        }


        public async Task OnGet()
        {
            SignedInUser = await _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> OnPostChangeUsername()
        {
            SignedInUser = await _userManager.GetUserAsync(HttpContext.User);

            ApplicationUser user = await _userManager.FindByNameAsync(SignedInUser.UserName);

            user.UserName = NewUsername;

            var updateUserResult = await _userManager.UpdateAsync(user);

            if (updateUserResult.Succeeded)
            {
                return RedirectToPage("/Member/Index");

            }
            else
            {
                return Page();
            }


        }

        public async Task<IActionResult> OnPostDelete()
        {
            SignedInUser = await _userManager.GetUserAsync(HttpContext.User);

           var removeUser = await _userManager.DeleteAsync(SignedInUser);

            if (removeUser.Succeeded)
            {
                return RedirectToPage("/Member/Index");

            }

            else
            {
                return Page();
            }

        }

    }
}
