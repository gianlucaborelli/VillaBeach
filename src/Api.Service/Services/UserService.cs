using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<UserDto> Get(Guid id)
        {
            var entity =  await _repository.SelectAsync(id);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var entity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(entity);
        }

        public async Task<IEnumerable<UserDto>?> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var entityList= await _repository.FindByName(name);

            return _mapper.Map<IEnumerable<UserDto>?>(entityList);
            
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            var model= _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdateRequest user)
        {
            var model= _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}