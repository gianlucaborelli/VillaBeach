using Api.Service.Interfaces;
using Api.Domain.Interface;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserSettingsService(IMapper mapper) : IUserSettingsService
    {
        private readonly IMapper _mapper = mapper;

        public Dictionary<string, int> GetSettingByUserId()
        {
            throw new NotImplementedException();
            // var response = _repository.GetByIdAsync(_auth.GetUserId());
            // return _mapper.Map<Dictionary<string, int>>(response);
        }

        public async Task<bool> UpdateSetting(string key, int value)
        {
            throw new NotImplementedException();
            // var response = await _repository.GetByIdAsync(_auth.GetUserId()) 
            //                 ?? throw new AuthenticationException("User not found.") ;

            // var settingsProperty = response.Settings.GetType().GetProperty(key) 
            //                 ?? throw new ArgumentException("Property not found");

            // settingsProperty.SetValue(response.Settings, value);

            // _repository.Update(response);

            // return true;            
        }
    }
}