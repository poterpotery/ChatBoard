using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DTO.Enums;
using DTO.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private readonly DBContext _db;

        public AccountRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Account> GetByIdAsync(long id)
        {
            return await FindByCondition(f => f.Id == id && !f.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByIdAsync(long id)
        {
            return await FindAll().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetUserByIdAsync(long id)
        {

            return await FindByCondition(f => f.Id == id  && !f.IsDeleted).FirstOrDefaultAsync();
        }
        public IQueryable<Account> GetAllAccountAsync()
        {
            return FindAll().Where(f => !f.IsDeleted).OrderBy(x => x.CreatedAt);
        }
        public IQueryable<Account> GetAllUserAsync()
        {
            return FindAll().Where(f => !f.IsDeleted);
        }

        public IQueryable<Account> GetAllBlockedAccountAsync(List<long> Accountid)
        {
            return FindAll().Where(f => Accountid.Contains(f.Id) && !f.IsDeleted).OrderBy(x => x.CreatedAt);
        }

        public async Task<List<Account>> GetAllAccountByEmailAsync(string email)
        {
            return await FindAll().Where(a => a.Email.ToLower().Contains(email.ToLower()) && !a.IsDeleted).ToListAsync();
        }
        public async Task<Account> GetVerfiyAccountByIdAsync(long id)
        {
            return await FindByCondition(f => f.Id == id && !f.IsDeleted && f.IsEmailVerified && f.IsVerifiedAccount ).FirstOrDefaultAsync();
        }

        public bool AnyAccountByEmail(string email)
        {
            return FindByCondition(f => f.Email.Equals(email)).Any();
        }

        public bool AnyAccountByEmail(string email, long id)
        {
            return FindByCondition(f => f.Email.Equals(email) && f.Id != id).Any();
        }

        public Account GetAccountByEmail(string email, EAccountType accountType)
        {
            return FindByCondition(f => f.Email.Equals(email) && f.AccountType == accountType && f.IsDeleted == false).FirstOrDefault();
        }
        public Account GetAccountByEmailTest(string email)
        {
            return FindByCondition(f => f.Email.Equals(email)).FirstOrDefault();
        }

        public Account GetAccountByUserName(string userName)
        {
            return FindByCondition(f => f.Username.ToLower().Equals(userName.ToLower())).FirstOrDefault();
        }
        public Account GetAccountByUserNameAndAccountId(string userName, long AccountId)
        {
            return FindByCondition(f => f.Username.ToLower().Equals(userName.ToLower()) && f.Id != AccountId).FirstOrDefault();
        }
        public Account GetAccountByEmailPwd(string email, string password)
        {
            return FindByCondition(f => f.Email.Equals(email) && f.Password.Equals(password)).FirstOrDefault();
        }
        public Task<Account> GetAccountById(long Id)
        {
            return FindByCondition(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return FindAll().Where(ac => !ac.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);
        }

        public IEnumerable<Account> GetAllAccountsEn()
        {
            return FindAll().Where(ac => !ac.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);
        }
    }
}