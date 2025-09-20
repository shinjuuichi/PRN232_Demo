using WebMVC.Models;

namespace WebMVC.Services
{
    public interface IUserApiClient
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> Create(User user);
        Task Update(int id, User user);
        Task Delete(int id);
    }
}