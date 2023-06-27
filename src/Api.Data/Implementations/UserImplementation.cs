using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {

        private DbSet<UserEntity> _dataSet;

        public UserImplementation(MyContext context):base(context)
        {
            _dataSet = context.Set<UserEntity>();
        }

        public async Task<IEnumerable<UserEntity>?> FindByName(string name)
        {
            return await _dataSet.Where(u => u.Name.Contains(name)).ToListAsync();
        }
    }
}