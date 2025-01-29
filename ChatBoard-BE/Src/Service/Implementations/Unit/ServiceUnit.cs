using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Interfaces.Unit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Service.Implementations.Unit
{
    internal class ServiceUnit : IServiceUnit
    {
        private readonly IRepositoryUnit _repository;
        private readonly IMapper _mapper;
        private readonly IEventLogger _eventLogger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileManagementService _fileManagement;
        private readonly IMemoryCache _cache;
        private IConfiguration _configuration;
        private IEmailServices _email;
        IAccountService _accountService;
        IAccountBlockService _AccountBlock;
        public ServiceUnit(IRepositoryUnit repository, IFileManagementService fileManagementService, IMapper mapper, IEventLogger eventLogger, IWebHostEnvironment hostingEnvironment
           , IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _eventLogger = eventLogger;
            _hostingEnvironment = hostingEnvironment;
            _fileManagement = fileManagementService;
            _cache = cache;
        }

        public IEmailServices Email =>
            _email ??= new EmailServices(_hostingEnvironment, _eventLogger);

        public IAccountService Account =>
            _accountService ??= new AccountService(_repository, Email, _eventLogger, _mapper, _fileManagement, _hostingEnvironment);
    }
}
