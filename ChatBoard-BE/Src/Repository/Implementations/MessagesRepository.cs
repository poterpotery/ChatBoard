using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DTO.Model;
using DTO.ViewModel.Messages;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
	internal class MessagesRepository : RepositoryBase<Messages>, IMessagesRepository
    {
        private readonly DBContext _db;

        public MessagesRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<PaginatedList<Messages>> GetMessagesAsync(long userId, long otherUserId, int pageNumber, int pageSize)
        {
            var query = FindAll()
                .Where(m => (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderByDescending(m => m.SentAt);

            var totalMessages = await query.CountAsync();

            var messages = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Messages>(messages, totalMessages, pageNumber, pageSize);
        }

    }
}