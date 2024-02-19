using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.UI.Pages.Message
{
	[BindProperties]
	public class MessageBoardModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly AppDbContext _appDbContext;
		private readonly AuthDbContext _context;

		public List<MessageModel> Messages { get; set; }

		public bool IsAdmin { get; set; }
		public ApplicationUser loggedInUser { get; set; }

		public MessageBoardModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AuthDbContext context)
		{
			_context = context;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public async Task OnGet()
		{
			// 1. Hämta nuvarande inloggade användaren
			loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

			// 2. Hämta alla messages
			var messages = _context.Messages.Include(m => m.User).ToList();
			messages.Sort((x, y) => DateTime.Compare(y.Date, x.Date));
			Messages = messages;

		}

		public async Task<IActionResult> OnPostAdminRole()
		{
			bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");

			// Om Admin-rollen inte existerar redan...
			if (!adminRoleExists)
			{
				// 1. Skapa ett Admin-roll objekt
				IdentityRole adminRole = new()
				{
					Name = "Admin"
				};

				// 2. Lägg till den Admin-rollen i databasen
				var createAdminRoleResult = await _roleManager.CreateAsync(adminRole);

				if (createAdminRoleResult.Succeeded)
				{
					adminRoleExists = true;
				}
			}

			if (adminRoleExists)
			{
				// 3. Hämta den nuvarande inloggade användaren
				ApplicationUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

				// 4. Lägg till Admin-rollen till den nuvarande inloggade användaren
				var addToAdminRoleResult = await _userManager.AddToRoleAsync(loggedInUser, "Admin");

				if (addToAdminRoleResult.Succeeded)
				{
					return RedirectToPage("/Admin/Index");
				}
			}

			return Page();
		}

	}
}
