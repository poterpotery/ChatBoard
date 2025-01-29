using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class DeviceActivity : CommonDbProps
    {
        public long AccountId { get; set; }
        public Account Account { get; set; }
        public string Operation { get; set; }
    }
}
