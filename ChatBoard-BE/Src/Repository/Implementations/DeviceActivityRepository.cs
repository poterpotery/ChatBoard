using System;
using Context;
using DTO.Model;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
	internal class DeviceActivityRepository : RepositoryBase<DeviceActivity>, IDeviceActivityRepository
    {
        private readonly DBContext _db;

        public DeviceActivityRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }
    }
}