using System;
using DTO.Enums;

namespace DTO.ViewModel.Token
{
    public class JwtTokenModel
    {
        private DateTime FromUnixTime(long unixTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(unixTime);
        }

        public JwtTokenModel() { }

        public JwtTokenModel(long id)
        {
            Id = id;
        }

        public JwtTokenModel(long id, long issuedAt,
            long expiresAt, long notValidBefore, EAccountType type)
        {
            Id = id;
            IssuedAtEpoch = issuedAt;
            IssuedAt = FromUnixTime(IssuedAtEpoch);
            ExpiresAtEpoch = expiresAt;
            ExpiresAt = FromUnixTime(expiresAt);
            NotValidBeforeEpoch = notValidBefore;
            NotValidBefore = FromUnixTime(notValidBefore);
            Type = type;
        }

        public long Id { get; set; }

        public long IssuedAtEpoch { get; set; }

        public DateTime IssuedAt { get; set; }

        public long ExpiresAtEpoch { get; set; }

        public DateTime ExpiresAt { get; set; }

        public long NotValidBeforeEpoch { get; set; }

        public DateTime NotValidBefore { get; set; }

        public EAccountType Type { get; set; }

    }

    public static class TokenClaimKeys
    {
        public const string Type = "x-type";

        public const string Value = "x-value";

        public const string IssuedAt = "iat";

        public const string ExpiresAt = "exp";

        public const string NotValidBefore = "nbf";
    }
}