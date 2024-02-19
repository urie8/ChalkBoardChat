using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Member
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public string Message { get; set; }

        //private readonly Uow _uow;
        private readonly AppDbContext _appDbContext;
        private readonly AuthDbContext _context;

        public bool IsAdmin { get; set; }

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext appContext, AuthDbContext context)
        {
            _appDbContext = appContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;

            //_uow = uow;
        }

        public async Task OnGet()
        {
            // 1. Hämta nuvarande inloggade användaren
            ApplicationUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            // 2. Kolla om den nuvarande inloggade användaren har en Admin-roll och sätt IsAdmin-propertyn därefter
            IsAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
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

                //_userManager.IsInRoleAsync(loggedInUser, "Admin");

                if (addToAdminRoleResult.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return Page();
            }
            else
            {
                ApplicationUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

                MessageModel newMessageModel = new()
                {
                    Message = Message,
                    Date = DateTime.Now,
                    User = loggedInUser
                };

                _context.Add(newMessageModel);
                _context.SaveChanges();

                //_uow.MessagesRepo.Add(newMessageModel);
                //_uow.Complete();

                return Page();
            }
        }
    }
}
