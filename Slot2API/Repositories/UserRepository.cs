using Microsoft.EntityFrameworkCore;
using Slot2API.Data;
using Slot2API.Models;

namespace Slot2API.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly Slot2APIContext _context;

        public UserRepository(Slot2APIContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task Update(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
