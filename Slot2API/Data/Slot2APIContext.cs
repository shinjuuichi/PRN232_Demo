using Microsoft.EntityFrameworkCore;
using Slot2API.Models;

namespace Slot2API.Data
{
    public class Slot2APIContext : DbContext
    {
        public Slot2APIContext(DbContextOptions<Slot2APIContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
    }
}
