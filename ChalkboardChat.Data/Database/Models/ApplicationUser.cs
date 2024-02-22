using Microsoft.AspNetCore.Identity;

namespace ChalkboardChat.Data.Database.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();

    }
}
