using System.Threading.Tasks;

namespace Repository.Interfaces.Unit
{
    public interface IRepositoryUnit
    {
        IAccountRepository Account { get; }
        IAccountConfirmationRepository AccountConfirmation { get; }
        IUserDeviceRepository UserDevice { get; }
        IDeviceActivityRepository DeviceActivity { get; }
        IConstantSettingRepository ConstantSetting {  get; }
        IMessagesRepository Messages {  get; }

        void DetachAllEntities();
        
        void Save();

        Task SaveAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        Task BeginTransactionAsync();
    }
}
