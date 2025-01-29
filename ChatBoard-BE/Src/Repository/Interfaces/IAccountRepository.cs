using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Enums;
using DTO.Model;
using Repository.Interfaces.Base;

namespace Repository.Interfaces
{
	public interface IAccountRepository : IRepositoryBase<DTO.Model.Account>
    {
        Task<Account> GetByIdAsync(long id);
        Task<Account> GetAccountByIdAsync(long id);
        Task<Account> GetUserByIdAsync(long id);
        IQueryable<Account> GetAllUserAsync();
        IQueryable<Account> GetAllAccountAsync();
        IQueryable<Account> GetAllBlockedAccountAsync(List<long> AccountId);
        Task<List<Account>> GetAllAccountByEmailAsync(string email);
        Task<Account> GetVerfiyAccountByIdAsync(long id);
        Account GetAccountByEmail(string email, EAccountType accountType);
        bool AnyAccountByEmail(string email);
        bool AnyAccountByEmail(string email, long id);
        Account GetAccountByEmailPwd(string email, string password);
        Task<Account> GetAccountById(long Id);
        Account GetAccountByUserName(string userName);
        Account GetAccountByUserNameAndAccountId(string userName, long AccountId);
        IQueryable<Account> GetAllAccounts();
        Account GetAccountByEmailTest(string email);
        IEnumerable<Account> GetAllAccountsEn();
    }
}