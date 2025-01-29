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
	internal class AccountConfirmationRepository : RepositoryBase<AccountConfirmation>, IAccountConfirmationRepository
    {
        private readonly DBContext _db;

        public AccountConfirmationRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }

        public AccountConfirmation GetById(long id)
        {
            return FindByCondition(f => !f.IsDeleted && f.Id == id).FirstOrDefault();
        }
        public Task<AccountConfirmation> GetAccountConfirmationWithAccountByToken(string token) => FindByConditionWithTracking(x => x.IsDeleted == false && x.IsUsed == false && x.Code == token).Include(x => x.Account).FirstOrDefaultAsync();
        public Task<List<AccountConfirmation>> GetOTPAganistAccountId(long AccountId)
        {
            return FindByCondition(f => !f.IsDeleted && f.AccountId == AccountId && !f.IsUsed).ToListAsync();
        }
    }
}