using System.Security.Authentication;
using Api.Service.Interfaces;
using Api.Domain.Interface;
using AutoMapper;
using Api.CrossCutting.Identity.Authentication;


namespace Api.Service.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private IUserRepository _repository;
        //private IAuthenticationService _auth;

        private readonly IMapper _mapper;

        public UserSettingsService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //_auth = auth;
        }

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