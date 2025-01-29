using Common.Helpers;
using DTO.Enums;
using DTO.ViewModel.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class LimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        //private readonly VertexContext _repository;
        public LimitMiddleware(RequestDelegate next, IMemoryCache cache/*, VertexContext repository*/)
        {
            _next = next;
            _cache = cache;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenModel = ValidateToken(token);
            //var account = await _repository.Accounts.FindAsync(tokenModel.Id);
            //if (account == null) throw new Exception("Account doesn’t exist", null);           
            //if (account.IsBlock) throw new Exception("Your account is blocked", null);

            var targetEndpointpost = "/api/v1/post/createPostThreads";
            var targetEndpointpostComment = "/api/v1/post/createComment";
            if (context.Request.Path.StartsWithSegments(targetEndpointpost) && context.Request.Method == HttpMethods.Post)
            {
                //var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                //var tokenModel = ValidateToken(token);
                if (tokenModel != null)
                {
                    var ipAddress = context.Connection.RemoteIpAddress.ToString();
                    var cacheKey = $"{ipAddress}_{tokenModel.Id}_RateLimit";
                    var requestCount = _cache.TryGetValue(cacheKey, out int count) ? count : 0;
                    if (requestCount >= 2)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new
                        {
                            Data = (object)null,
                            Error = new
                            {
                                ErrorCode = "UnhandledError",
                                ErrorMessage = "Limit exceeded. Try again later."
                            },
                            IsSuccess = false,
                            Message = "Limit exceeded"
                        };
                        var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                        await context.Response.WriteAsync(jsonResponse);
                        return;
                    }
                    _cache.Set(cacheKey, requestCount + 1, TimeSpan.FromMinutes(1)); // Reset count every 1 minute
                }
            }
            else if (context.Request.Path.StartsWithSegments(targetEndpointpostComment) && context.Request.Method == HttpMethods.Post)
            {
                 if (tokenModel != null)
                {

                    var ipAddress = context.Connection.RemoteIpAddress.ToString();
                    var cacheKey = $"{ipAddress}_{tokenModel.Id}_RateLimit";
                    var requestCount = _cache.TryGetValue(cacheKey, out int count) ? count : 0;
                    if (requestCount >= 10)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new
                        {
                            Data = (object)null,
                            Error = new
                            {
                                ErrorCode = "UnhandledError",
                                ErrorMessage = "Limit exceeded. Try again later."
                            },
                            IsSuccess = false,
                            Message = "Limit exceeded"
                        };
                        var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                        await context.Response.WriteAsync(jsonResponse);
                        return;
                    }
                    _cache.Set(cacheKey, requestCount + 1, TimeSpan.FromMinutes(1)); // Reset count every 1 minute
                }
            }
            await _next(context);
        }
         public JwtTokenModel ValidateToken(string token)
        {
            try
            {
                ClaimsPrincipal principal = GetPrincipal(token);
                if (principal == null)
                    return null;
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

                // Token values
                var id = long.Parse(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(), identity.FindFirst(TokenClaimKeys.Value).Value));

                var issuedAt = long.Parse(identity.FindFirst(TokenClaimKeys.IssuedAt).Value);

                var expiresAt = long.Parse(identity.FindFirst(TokenClaimKeys.ExpiresAt).Value);

                var notValidBefore = long.Parse(identity.FindFirst(TokenClaimKeys.NotValidBefore).Value);

                var type = int.Parse(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(),
                        identity.FindFirst(TokenClaimKeys.Type).Value));

                return new JwtTokenModel(id, issuedAt, expiresAt, notValidBefore, (EAccountType)type);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
        private static ClaimsPrincipal GetPrincipal(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            if (jwtToken == null)
                return null;
            byte[] key = Encoding.ASCII.GetBytes(AppSettingHelper.GetJwtTokenSecret());
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            return tokenHandler.ValidateToken(token,
                parameters, out _);
        }
    }
}
