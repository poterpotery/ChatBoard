using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Numerics;

namespace Common.Helpers
{
    public static class AppSettingHelper
    {

        private static string GetSettingValue(string parentKey, string childKey)
        {
            try
            {
                IConfigurationRoot configuration = GetSettingConfiguration();

                if (!configuration.GetSection(parentKey).Exists())
                {
                    throw new Exception(MessageHelper.AppSettingMissing(parentKey));
                }

                if (!configuration.GetSection(parentKey).GetSection(childKey).Exists())
                {
                    throw new Exception(MessageHelper.AppSettingMissing(childKey));
                }

                return configuration.GetSection(parentKey).GetSection(childKey).Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private static IConfigurationRoot GetSettingConfiguration()
        {
            try
            {
                return new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                            .Build();

                //return new ConfigurationBuilder()
                //            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //            .AddJsonFile($"appsettings.json")
                //            .Build();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }


        // Parent Section
        public const string Portal = "Portal";

        public const string CustomSettings = "Custom_Settings";

        // Child Section
        public const string JwtTokenSecret = "Jwt_Token_Secret";

        public const string JwtValueSecret = "Jwt_Value_Secret";

        public const string ApiToken = "Api_Token";

        public const string PasswordSalt = "Password_Salt";

        public const string PasswordSecret = "Password_Secret";

        public const string EnableSignature = "Enable_Signature";

        public const string EnableSwagger = "Enable_Swagger";

        public const string EnableSeeder = "Enable_Seeder";
        public const string SuperAdmin = "SuperAdmin";
        public const string SubAdmin = "SubAdmin";
        // Smtp Settings
        public static string GetSmtpServerName() => GetSettingValue("Smtp", "ServerName");
        public static int GetSmtpServerPort() => int.Parse(GetSettingValue("Smtp", "ServerPort"));
        public static string GetSmtpEmailAddress() => GetSettingValue("Smtp", "EmailAddress");
        public static string GetSmtpEmailPassword() => GetSettingValue("Smtp", "EmailPassword");
        public static string GetSmtpEmailFrom() => GetSettingValue("Smtp", "EmailFrom");

        public static string GetTwilioAccountSid() => GetSettingValue("Twilio", "AccountSid");

        public static string GetTwilioToken() => GetSettingValue("Twilio", "Token");

        public static string GetTwilioNumber() => GetSettingValue("Twilio", "Number");
        public static string GetConfirmEmailCallback => GetSettingValue("Links", "ConfirmEmailCallback");
        public static string GetForgotEmailCallback => GetSettingValue("Links", "ForgotEmailCallback");
        public static string GetMasterKey => GetSettingValue("MasterKey", "key");
        public static bool GetEnableSeeder()
        {
            return bool.Parse(GetSettingValue(CustomSettings, EnableSeeder));
        }
        public static bool GetEnableSwagger()
        {
            return bool.Parse(GetSettingValue(CustomSettings, EnableSwagger));
        }
        public static bool GetEnableSignature()
        {
            return bool.Parse(GetSettingValue(CustomSettings, EnableSignature));
        }
        public static string GetJwtTokenSecret()
        {
            return GetSettingValue(CustomSettings, JwtTokenSecret);
        }

        public static string GetJwtValueSecret()
        {
            return GetSettingValue(CustomSettings, JwtValueSecret);
        }
        
        public static string GetApiToken()
        {
            return GetSettingValue(CustomSettings, ApiToken);
        }

        public static string GetMasterPartialKey()
        {
            return GetSettingValue(CustomSettings, "MasterPartialKey");
        }

        public static string GetTrippleDesToken()
        {
            return GetSettingValue(CustomSettings, "TrippleDesToken");
        }

        public static string GetPasswordSalt()
        {
            return GetSettingValue(CustomSettings, PasswordSalt);
        }

        public static string GetPasswordSecret()
        {
            return GetSettingValue(CustomSettings, PasswordSecret);
        }
        public static string GetDefaultConnection()
        {
            return GetSettingValue("ConnectionStrings", "DefaultConnection");
        }
    }
}