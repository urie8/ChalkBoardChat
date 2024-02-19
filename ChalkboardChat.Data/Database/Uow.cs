namespace ChalkboardChat.Data.Database
{
    public class Uow
    {
        private readonly AppDbContext _context;
        public MessagesRepo MessagesRepo { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
