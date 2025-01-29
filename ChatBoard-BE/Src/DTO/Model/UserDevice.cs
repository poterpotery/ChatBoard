using System;
namespace DTO.Model
{
    public class UserDevice : CommonDbProps
    {
        public long AccountId { get; set; }
        public Account Account { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
        public string PlayerID { get; set; }
        public string VoiPPlayerId { get; set; }
    }
}

