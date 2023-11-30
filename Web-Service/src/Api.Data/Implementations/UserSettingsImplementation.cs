using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserSettingsImplementation : BaseRepository<UserSettingsEntity>, IUserSettingsRepository
    {
        private DbSet<UserSettingsEntity> _dataSet;

        public UserSettingsImplementation(MyContext context):base(context)
        {
            _dataSet = context.Set<UserSettingsEntity>();
        }

        public async Task<UserSettingsEntity?> GetSettingByUserId(Guid id)
        {
            return await _dataSet.Where(x => x.UserId.Equals(id)).SingleOrDefaultAsync();
        }
    }
}