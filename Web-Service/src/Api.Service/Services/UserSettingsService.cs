using Api.Domain.Interfaces.Services.Login;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using AutoMapper;


namespace Api.Service.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private IUserSettingsRepository _repository;
        private ILoginService _auth;

        private readonly IMapper _mapper;

        public UserSettingsService(IUserSettingsRepository repository, IMapper mapper, ILoginService auth)
        {
            _repository = repository;
            _mapper = mapper;
            _auth = auth;
        }

        public Dictionary<string, int> GetSettingByUserId()
        {
            var response = _repository.GetSettingByUserId(_auth.GetUserId());
            return _mapper.Map<Dictionary<string, int>>(response);
        }

        public async Task<bool> UpdateSetting(string key, int value)
        {
            var response = await _repository.GetSettingByUserId(_auth.GetUserId());

            response.GetType().GetProperty(key).SetValue(response, value);

            await _repository.UpdateAsync(response);

            return true;            
        }
    }
}