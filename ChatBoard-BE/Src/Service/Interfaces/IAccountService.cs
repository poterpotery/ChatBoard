

using DTO.Model;
using DTO.ViewModel;
using DTO.ViewModel.Account;
using DTO.ViewModel.Messages;
using DTO.ViewModel.Token;
using Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult<AuthModel>> SignUpAsync(SignUpRequestModel model);
        Task<ServiceResult<AuthModel>> LoginAsync(LoginRequestModel model);
        Task<ServiceResult<Messages>> SendMessageAsync(long Id, SendMessageRequest request);
        Task<ServiceListResult<List<MessageDto>>> GetMessagesAsync(long Id, GetMessagesRequest request);
        Task<ServiceListResult<List<UserDto>>> GetUsersAsync(long Id, PaginationModel request);
        JwtTokenModel ValidateToken(string token);

        Task<ServiceResult<AccountViewModel>> UpdateAccountAsync(long accountId, UpdateAccountRequestModel model);
        Task<ServiceResult<AccountViewModel>> GetAccountById(long UserAccountId);
        Task<ServiceResult<AccountViewModel>> Current(long accountId);
        Task<ServiceResult<string>> DeleteAccount(long accountId);
    }
}

