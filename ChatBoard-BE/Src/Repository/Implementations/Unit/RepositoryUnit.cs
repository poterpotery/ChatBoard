using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Repository.Interfaces;
using Repository.Interfaces.Unit;

namespace Repository.Implementations.Unit
{
    public class RepositoryUnit : IRepositoryUnit
    {
        private readonly DBContext _db;

        public RepositoryUnit(DBContext db)
        {
            _db = db;
        }

        IAccountRepository _account;
        IAccountConfirmationRepository _accountconfirmation;
        IUserDeviceRepository _userdevice;
        IDeviceActivityRepository _deviceactivity;
        IMessagesRepository _messages;
        private IConstantSettingRepository _ConstantSetting;

        public IMessagesRepository Messages =>
            _messages ??= new MessagesRepository(_db);

        public IConstantSettingRepository ConstantSetting =>
             _ConstantSetting ??= new ConstantSettingRepository(_db);

        public IAccountRepository Account =>
            _account ??= new AccountRepository(_db);

        public IAccountConfirmationRepository AccountConfirmation =>
            _accountconfirmation ??= new AccountConfirmationRepository(_db);

        public IUserDeviceRepository UserDevice =>
            _userdevice ??= new UserDeviceRepository(_db);

        public IDeviceActivityRepository DeviceActivity =>
            _deviceactivity ??= new DeviceActivityRepository(_db);

        public void Save()
        {
            //SetCommonValues();
            _db.SaveChanges();
        }

        public void SetCommonValues()
        {
            var AddedEntities = _db.ChangeTracker.Entries()
        .Where(E => E.State == EntityState.Added)
        .ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("createdAt").CurrentValue = E.Property("updatedAt").CurrentValue = DateTime.UtcNow;
            });

            var EditedEntities = _db.ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("updatedAt").CurrentValue = DateTime.UtcNow;
            });
        }

        public async Task SaveAsync()
        {
            //SetCommonValues();
            await _db.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _db.Database.BeginTransactionAsync();
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = _db.ChangeTracker.Entries()
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
