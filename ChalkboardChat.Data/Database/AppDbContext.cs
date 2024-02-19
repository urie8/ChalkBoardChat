using ChalkboardChat.Data.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<MessageModel> Messages { get; set; }

    }
}
