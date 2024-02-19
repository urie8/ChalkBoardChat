using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages
{
	public class IndexModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AuthDbContext _authDbContext;

		public string? Username { get; set; }
		public string? Password { get; set; }

		public ApplicationUser SignedInUser { get; set; }

		public IndexModel(SignInManager<ApplicationUser> signInManager, AuthDbContext authDbContex, UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_authDbContext = authDbContex;
			_userManager = userManager;
		}

		public async Task OnGet()
		{
			// 1. Hämta nuvarande inloggade användaren
			SignedInUser = await _userManager.GetUserAsync(HttpContext.User);


		}

	}
}
