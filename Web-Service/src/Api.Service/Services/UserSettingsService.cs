using System.Security.Authentication;
using Api.Domain.Interfaces.Services.Authentication;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using AutoMapper;


namespace Api.Service.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private IUserRepository _repository;
        private IAuthenticationService _auth;

        private readonly IMapper _mapper;

        public UserSettingsService(IUserRepository repository, IMapper mapper, IAuthenticationService auth)
        {
            _repository = repository;
            _mapper = mapper;
            _auth = auth;
        }

        public Dictionary<string, int> GetSettingByUserId()
        {
            var response = _repository.FindById(_auth.GetUserId());
            return _mapper.Map<Dictionary<string, int>>(response);
        }

        public async Task<bool> UpdateSetting(string key, int value)
        {
            var response = await _repository.FindById(_auth.GetUserId()) 
                            ?? throw new AuthenticationException("User not found.") ;

            response.GetType().GetProperty(key)!.SetValue(response, value);

            await _repository.UpdateAsync(response);

            return true;            
        }
    }
}