using Microsoft.EntityFrameworkCore;

namespace MinApi.Services
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {

        }
        protected TodoDbContext()
        {
        }
        public DbSet<TodoItem> TodoItmes { get; set; }
    }
}
