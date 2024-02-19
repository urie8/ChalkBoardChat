using ChalkboardChat.Data.Database.Models;

namespace ChalkboardChat.Data.Database
{
    public class MessagesRepo : GenericAppRepo<MessageModel>
    {
        AppDbContext _context;
        public MessagesRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
