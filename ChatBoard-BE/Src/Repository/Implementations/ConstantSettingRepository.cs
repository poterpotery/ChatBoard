using Context;
using DTO.Enums;
using DTO.Model;
using DTO.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    internal class ConstantSettingRepository : RepositoryBase<ConstantSetting>, IConstantSettingRepository
    {
        private readonly DBContext _db;

        public ConstantSettingRepository(DBContext db)
            : base(db)
        {
            _db = db;
        }
    }
}