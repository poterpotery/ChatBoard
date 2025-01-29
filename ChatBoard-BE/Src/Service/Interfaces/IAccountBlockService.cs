using Service.Models;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAccountBlockService
    {
        Task<ServiceResult<string>> UserBlock(long LoginAccountid, long BlockAccountId);
        Task<ServiceResult<string>> UserUnBlock(long LoginAccountid, long BlockAccountId);
      
    }
}
