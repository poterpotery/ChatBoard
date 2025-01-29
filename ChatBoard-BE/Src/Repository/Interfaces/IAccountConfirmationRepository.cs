using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Model;
using Repository.Interfaces.Base;

namespace Repository.Interfaces
{
	public interface IAccountConfirmationRepository : IRepositoryBase<AccountConfirmation>
    {
        AccountConfirmation GetById(long id);
        Task<AccountConfirmation> GetAccountConfirmationWithAccountByToken(string token);
        Task<List<AccountConfirmation>> GetOTPAganistAccountId(long AccountId);
    }
}

