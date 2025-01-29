using System;
using System.Threading.Tasks;
using DTO.Model;
using DTO.ViewModel.Messages;
using Repository.Interfaces.Base;

namespace Repository.Interfaces
{
	public interface IMessagesRepository: IRepositoryBase<Messages>
	{
        Task<PaginatedList<Messages>> GetMessagesAsync(long userId, long otherUserId, int pageNumber, int pageSize);

    }
}

