using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DTO.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
    internal class UserDeviceRepository : RepositoryBase<UserDevice>, IUserDeviceRepository
    {
        private readonly DBContext _db;

        public UserDeviceRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<UserDevice> GetByAccountIdAndDeviceAsync(long accountId, string device)
        {
            return await FindByCondition(f => f.AccountId == accountId && f.PlayerID.ToLower().Equals(device.ToLower()) && f.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<UserDevice> GetByAccountIdAsync(long accountId)
        {
            return await FindByCondition(f => f.AccountId == accountId && f.IsDeleted == false).FirstOrDefaultAsync();
        }
        public IQueryable<UserDevice> GetAccountsPlayerids(List<long> accountIds)
        {
            return FindAll().Where(ac => accountIds.Contains(ac.AccountId) && !ac.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);
        }
        public async Task<List<UserDevice>> GetUserDevicesByAccountId(long accountId)
        {
            return await FindAll().Where(ac => accountId == ac.AccountId && !ac.IsDeleted)
                .OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}