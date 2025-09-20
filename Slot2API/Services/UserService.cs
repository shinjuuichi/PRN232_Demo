using AutoMapper;
using Slot2API.DTOs;
using Slot2API.Models;
using Slot2API.Repositories;

namespace Slot2API.Services
{
    public class UserService(IUserRepository _repo, IMapper _mapper) : IUserService
    {
        public async Task<GetUserDTO> Create(CreateUserDTO req)
        {
            var entity = _mapper.Map<User>(req);

            await _repo.Add(entity);

            return _mapper.Map<GetUserDTO>(entity);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<IEnumerable<GetUserDTO>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<IEnumerable<GetUserDTO>>(entities);
        }

        public async Task<GetUserDTO?> GetById(int id)
        {
            var entity = await _repo.GetById(id);
            return entity is null ? null : _mapper.Map<GetUserDTO>(entity);
        }

        public async Task<GetUserDTO?> Update(int id, UpdateUserDTO req)
        {
            var entity = await _repo.GetById(id);
            if (entity is null) return null;

            _mapper.Map(req, entity);

            await _repo.Update(entity);

            return _mapper.Map<GetUserDTO>(entity);
        }
    }
}
