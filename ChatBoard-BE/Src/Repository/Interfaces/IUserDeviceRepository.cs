using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Model;
using Repository.Interfaces.Base;

namespace Repository.Interfaces
{
	public interface IUserDeviceRepository : IRepositoryBase<UserDevice>
    {
        Task<UserDevice> GetByAccountIdAndDeviceAsync(long accountId, string device);
        IQueryable<UserDevice> GetAccountsPlayerids(List<long> accountIds);
        Task<UserDevice> GetByAccountIdAsync(long accountId);
        Task<List<UserDevice>> GetUserDevicesByAccountId(long accountId);
    }
}

