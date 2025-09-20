using Slot2API.DTOs;

namespace Slot2API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDTO>> GetAll();
        Task<GetUserDTO> GetById(int id);
        Task<GetUserDTO> Create(CreateUserDTO user);
        Task<GetUserDTO> Update(int id, UpdateUserDTO user);
        Task Delete(int id);
    }
}
