using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Common.Helpers;
using DTO.Enums;
using DTO.Model;
using DTO.ViewModel;
using DTO.ViewModel.Account;
using DTO.ViewModel.Messages;
using DTO.ViewModel.Token;
using FirebaseAdmin.Messaging;
using Logger.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nethereum.ABI.CompilationMetadata;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;

namespace Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryUnit _repository;
        private readonly IEmailServices _emailServices;
        private readonly IEventLogger _eventLogger;
        private readonly IFileManagementService _fileManagementService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment hostingEnvironment;
        public AccountService(IRepositoryUnit repository, IEmailServices emailServices, IEventLogger eventLogger,
            IMapper mapper, IFileManagementService fileManagementService,
            IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            _emailServices = emailServices;
            _eventLogger = eventLogger;
            _mapper = mapper;
            _fileManagementService = fileManagementService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<ServiceResult<AuthModel>> SignUpAsync(SignUpRequestModel model)
        {
            try
            {
                var AlreadyAccount = _repository.Account.GetAccountByEmail(model.Email, model.AccountType);
                if (AlreadyAccount != null)
                {
                    return ServiceResults.Errors.AlreadyExist<AuthModel>("Account", null);
                }

                var EncryptedPWD = TrippleDES.Encrypt(model.Password, AppSettingHelper.GetTrippleDesToken());
                var usercode = Guid.NewGuid().ToString().Replace("-", "");
                var userName = usercode.Substring(0, 6);

                Account account = new Account()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = userName,
                    EGender = model.Gender,
                    Email = model.Email,
                    Password = EncryptedPWD,
                    AccountType = EAccountType.User,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                };

                // Add Email Verification
                var tempAC = new AccountConfirmation()
                {
                    Type = EAccountConfirmationType.EmailVerification,
                    Code = Guid.NewGuid().ToString().Replace("-", ""),
                    IsUsed = false,
                };

                account.AccountConfirmations.Add(tempAC);

                _repository.Account.Create(account);
                await _repository.SaveAsync();

                // return model
                AuthModel resultData = new AuthModel();
                return ServiceResults.Empty<AuthModel>("Welcome", resultData);
            }
            catch (Exception ex)
            {
                // log events if error happens
                await _eventLogger.LogEvent("", "User", "Sign up", ex);
                return ServiceResults.Errors.Invalid<AuthModel>(ex.Message, null);
            }
        }
        public async Task<ServiceResult<AuthModel>> LoginAsync(LoginRequestModel model)
        {
            try
            {
                var EncryptedPWD = TrippleDES.Encrypt(model.Password, AppSettingHelper.GetTrippleDesToken());

                var Account = _repository.Account.GetAccountByEmail(model.Email, model.AccountType);
                //var Account = _repository.Account.GetAccountByEmailPwd(model.Email, EncryptedPWD);
                if (Account == null)
                {
                    return ServiceResults.Empty<AuthModel>("Account doesn’t exist", null);
                }
                if (Account.Password != EncryptedPWD)
                {
                    return ServiceResults.Empty<AuthModel>("Email/password is invalid", null);
                }

                string token = string.Empty;
                // Generate JWT token
                token = GenerateToken(Account.Id, (int)Account.AccountType);
                // Just to validate token
                JwtTokenModel jwt = ValidateToken(token);
                // return model
                AuthModel resultData = new AuthModel()
                {
                    Token = token,
                    Account = new()
                    {
                        Id = Account.Id,
                        FirstName = Account.FirstName,
                        Username = Account.Username,
                        AccountType = Account.AccountType.ToString(),
                        Bio = Account.Bio,
                        DOB = Account.DOB,
                        Email = Account.Email,
                        Gender = Account.EGender.ToString(),
                        ProfileBannerImage = Account.ProfileBannerImage,
                        ProfileImage = Account.ProfileImage,
                        IsEmailVerified = Account.IsEmailVerified,
                        IsVerifiedAccount = Account.IsVerifiedAccount,
                    }
                };
                // log Login success call
                await _eventLogger.LogEvent(Account.Id.ToString(), "User", "Login", new[] { Account.Email });
                return ServiceResults.Empty("Successfully logged in", resultData);
            }
            catch (Exception ex)
            {
                // log events if error happens
                await _eventLogger.LogEvent("", "User", "Login", ex);
                return ServiceResults.Errors.Invalid<AuthModel>(ex.Message, null);
            }
        }

        public async Task<ServiceResult<AccountViewModel>> UpdateAccountAsync(long accountId,UpdateAccountRequestModel model)
        {
            try
            {
                // Validate if model is null
                if (model == null)
                {
                    return ServiceResults.Errors.Invalid<AccountViewModel>("Invalid request data", null);
                }

                // Fetch the existing account using the provided AccountId
                var existingAccount = await _repository.Account.GetByIdAsync(accountId);
                if (existingAccount == null)
                {
                    return ServiceResults.Errors.NotFound<AccountViewModel>("Account not found", null);
                }

                // Check if the new email is already in use by another account
                //if (!string.Equals(existingAccount.Email, model.Email, StringComparison.OrdinalIgnoreCase) &&
                //    !string.IsNullOrWhiteSpace(model.Email))
                //{
                //    var emailExists = _repository.Account.GetAccountByEmail(model.Email, existingAccount.AccountType);
                //    if (emailExists != null)
                //    {
                //        return ServiceResults.Errors.AlreadyExist<AccountViewModel>("Email already in use", null);
                //    }
                //}

                // Update account properties only if they are not null or empty
                if (!string.IsNullOrWhiteSpace(model.FirstName))
                {
                    existingAccount.FirstName = model.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(model.LastName))
                {
                    existingAccount.LastName = model.LastName;
                }
                //if (!string.IsNullOrWhiteSpace(model.E))
                //{
                //    existingAccount.Email = model.Email;
                //}
                if (model.Gender != null) // Assuming Gender is a nullable enum
                {
                    existingAccount.EGender = model.Gender;
                }

                //// Update password if provided and not empty
                //if (!string.IsNullOrWhiteSpace(model.Password))
                //{
                //    var encryptedPassword = TrippleDES.Encrypt(model.Password, AppSettingHelper.GetTrippleDesToken());
                //    existingAccount.Password = encryptedPassword;
                //}

                existingAccount.UpdatedAt = DateTime.UtcNow;

                // Save changes
                _repository.Account.Update(existingAccount);
                await _repository.SaveAsync();

                // Prepare response data
                var resultData = new AccountViewModel
                {
                    Id = existingAccount.Id,
                    FirstName = existingAccount.FirstName,
                    LastName = existingAccount.LastName,
                    Email = existingAccount.Email,
                    Username = existingAccount.Username,
                };

                return ServiceResults.UpdatedSuccessfully<AccountViewModel>(resultData);
            }
            catch (Exception ex)
            {
                // Log the exception
                await _eventLogger.LogEvent("", "User", "Update account", ex);

                // Return a detailed error response
                return ServiceResults.Errors.Invalid<AccountViewModel>($"An error occurred: {ex.Message}", null);
            }
        }
        public async Task<ServiceResult<AccountViewModel>> GetAccountById(long UserAccountId)
        {
            var account = await _repository.Account.GetByIdAsync(UserAccountId);
            if (account == null)
            {
                return ServiceResults.Empty<AccountViewModel>("Account doesn’t exist", null);
            }

            var res = new AccountViewModel()
            {
                Id = account.Id,
                FirstName = account.FirstName,
                Username = account.Username,
                Email = account.Email,
                Gender = account.EGender.ToString(),
            };

            return ServiceResults.GetSuccessfully(res);
        }
        public async Task<ServiceResult<AccountViewModel>> Current(long accountId)
        {
            var account = await _repository.Account.GetByIdAsync(accountId);
            if (account == null)
            {
                return ServiceResults.Empty<AccountViewModel>("Account doesn’t exist", null);
            }

            var res = new AccountViewModel()
            {
                Id = account.Id,
                FirstName = account.FirstName,
                Username = account.Username,
                Email = account.Email,
                Gender = account.EGender.ToString(),
            };

            return ServiceResults.GetSuccessfully(res);
        }
        public async Task<ServiceResult<string>> DeleteAccount(long accountId)
        {
            var account = await _repository.Account.GetByIdAsync(accountId);
            if (account == null)
            {
                return ServiceResults.Errors.Invalid<string>("Account doesn’t exist", null);
            }

            account.IsDeleted = true;
            account.DeletedAt = DateTime.UtcNow;

            _repository.Account.Update(account);
            await _repository.SaveAsync();

            return ServiceResults.DeletedSuccessfully("Account");
        }

        public async Task<ServiceResult<Messages>> SendMessageAsync(long Id, SendMessageRequest request)
        {
            try
            {
                // Validate sender and receiver
                var sender = await _repository.Account.GetByIdAsync(Id);
                var receiver = await _repository.Account.GetByIdAsync(request.ReceiverId);

                if (sender == null)
                {
                    return ServiceResults.Errors.Invalid<Messages>("Sender does not exist.", null);
                }

                if (receiver == null)
                {
                    return ServiceResults.Errors.Invalid<Messages>("Receiver does not exist.", null);
                }

                if (string.IsNullOrWhiteSpace(request.Content))
                {
                    return ServiceResults.Errors.Invalid<Messages>("Message content cannot be empty.", null);
                }

                // Create a new message
                var message = new Messages
                {
                    SenderId = Id,
                    ReceiverId = request.ReceiverId,
                    Content = request.Content,
                    SentAt = DateTime.UtcNow,
                    IsRead = false
                };

                try
                {
                    // Save message to the database
                    _repository.Messages.Create(message);
                    await _repository.SaveAsync();
                }
                catch (Exception dbEx)
                {
                    // Log database exceptions
                    return ServiceResults.Errors.Invalid<Messages>("Failed to save the message. Please try again later.", null);
                }

                // Return success
                return ServiceResults.AddedSuccessfully(message);
            }
            catch (Exception ex)
            {
                // Log unhandled exceptions
                await _eventLogger.LogEvent("Service", "SendMessage", ex.Message);
                return ServiceResults.Errors.Invalid<Messages>("An unexpected error occurred. Please try again.", null);
            }
        }
        public async Task<ServiceListResult<List<MessageDto>>> GetMessagesAsync(long Id, GetMessagesRequest request)
        {
            try
            {
                // Validate input
                if (Id <= 0 || request.OtherUserId <= 0)
                {
                    throw new ArgumentException("Invalid user IDs provided");
                    //return ServiceResults.Errors.Invalid<PaginatedList<MessageDto>>("Invalid user IDs provided.", null);
                }

                if (request.PageNumber <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid pagination parameters.");
                    //return ServiceResults.Errors.Invalid<PaginatedList<MessageDto>>("/*Invalid pagination parameters.*/", null);
                }

                // Fetch messages from the database
                var messages = await _repository.Messages.GetMessagesAsync(
                    Id,
                    request.OtherUserId,
                    request.PageNumber,
                    request.PageSize
                );

                if (messages == null || !messages.Items.Any())
                {
                    return ServiceResults.GetListSuccessfully<List<MessageDto>>(new List<MessageDto>(), 0);
                    //return ServiceResults.Errors.Invalid<PaginatedList<MessageDto>>("No messages found.", null);
                }

                // Map messages to DTO and add IsMine flag
                var mappedMessages = messages.Items.Select(m => new MessageDto
                {
                    MessageId = m.Id,
                    Content = m.Content,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    SentAt = m.SentAt,
                    IsMine = m.SenderId == Id // Check if the current user is the sender
                }).ToList();

                // Paginated response
                var paginatedMessages = new PaginatedList<MessageDto>(mappedMessages, messages.TotalCount, request.PageNumber, request.PageSize);

                return ServiceResults.GetListSuccessfully(mappedMessages, messages.TotalCount);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new ArgumentException("Failed to fetch messages. Please try again later.");
                //return ServiceResults.Errors.Invalid<PaginatedList<MessageDto>>("Failed to fetch messages. Please try again later.", null);
            }
        }

        public async Task<ServiceListResult<List<UserDto>>> GetUsersAsync(long Id, PaginationModel request)
        {
            try
            {
                // Validate pagination parameters
                if (request.CurrentPage <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid pagination parameters.");
                }

                // Fetch users where IsDeleted is false
                var query = _repository.Account.FindAll()
                    .Where(u => !u.IsDeleted && u.Id != Id)
                    .OrderBy(u => u.CreatedAt);

                var totalCount = await query.CountAsync();

                var users = await query
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                // Map to DTO
                var userDtos = users.Select(u => new UserDto
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,
                    Name = u.FirstName + " "+ u.LastName
                }).ToList();

                // Paginated response
                var paginatedUsers = new PaginatedList<UserDto>(userDtos, totalCount, request.CurrentPage, request.PageSize);

                return ServiceResults.GetListSuccessfully(userDtos, totalCount);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to retrieve users.");
            }
        }




        public async Task<ServiceResult<string>> IsUserNameExist(string userName)
        {
            var account = await _repository.Account.FindByCondition(x => x.Username == userName).FirstOrDefaultAsync();
            if (account != null)
            {
                return ServiceResults.Errors.AlreadyExist<string>("User Name", "");
            }
            return ServiceResults.GetSuccessfully("UserName not Exist");
        }
        public async Task<ServiceResult<AccountStatusResponse>> IsAccountStatus(long AccountId)
        {
            var account = await _repository.Account.GetByIdAsync(AccountId);
            if (account == null)
            {
                return ServiceResults.Empty<AccountStatusResponse>("Account doesn’t exist", null);
            }
            var response = new AccountStatusResponse();
            return ServiceResults.GetSuccessfully(response);
        }
        public async Task<ServiceResult<string>> EncCheck(string userName)
        {
            var check = TrippleDES.Encrypt(userName, AppSettingHelper.GetTrippleDesToken());
            return ServiceResults.GetSuccessfully(check);
        }
        
        public async Task<ServiceResult<string>> ChangePasswordByPassword(long id, ChangePasswordRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.OldPassword))
                return ServiceResults.Errors.Required<string>("Old Password", null);
            var OldEncryptedPWD = TrippleDES.Encrypt(model.OldPassword, AppSettingHelper.GetTrippleDesToken());

            if (model.OldPassword != model.NewPassword)
                return ServiceResults.Empty<string>("Password doesn’t match", null);

            var account = await _repository.Account.GetByIdAsync(id);
            if (account == null)
                return ServiceResults.Empty<string>("Account doesn’t exist", null);
            if (account.Password != OldEncryptedPWD)
                return ServiceResults.Errors.Invalid<string>("You can't enter Old Password", null);

            var NewEncryptedPWD = TrippleDES.Encrypt(model.NewPassword, AppSettingHelper.GetTrippleDesToken());

            account.Password = NewEncryptedPWD;
            _repository.Account.Update(account);
            await _repository.SaveAsync();
            _ = _eventLogger.LogEvent(account.Id.ToString(), "User", "Password changed", new { });
            return ServiceResults.UpdatedSuccessfully("Password changed");
        }
        public async Task<ServiceResult<string>> SendForgotEmail(ForgotPasswordRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return ServiceResults.Errors.Required<string>("Email", null);

            if (!DataValidationHelper.IsValidEmail(model.Email))
                return ServiceResults.Errors.Invalid<string>("Email", null);
            var account = _repository.Account.GetAccountByEmail(model.Email, model.AccountType);
            if (account == null || account.IsDeleted == true)
                return ServiceResults.Errors.NotFound<string>("User with this email", null);

            Random generator = new Random();

            var Ac = new AccountConfirmation()
            {
                Type = EAccountConfirmationType.ForgetPassword,
                AccountId = account.Id,
                Code = generator.Next(99999, 1000000).ToString("D6"),
                IsUsed = false
            };

            _repository.AccountConfirmation.Create(Ac);
            await _repository.SaveAsync();

            _ = _emailServices.SendForgotEmailAsync(account.Email, account.Email, Token: Ac.Code);

            return ServiceResults.AddedSuccessfully("Please check your email for change password link");
        }
        public async Task<ServiceResult<string>> ChangePasswordByToken(ChangeForgotPasswordRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Token))
                return ServiceResults.Errors.Required<string>("Token", null);
            var accountConfirmation = await _repository.AccountConfirmation.GetAccountConfirmationWithAccountByToken(model.Token);
            if (accountConfirmation == null)
                return ServiceResults.Errors.Invalid<string>("Activation token", null);
            if (accountConfirmation.Type != EAccountConfirmationType.ForgetPasswordConfirmation)
            {
                return ServiceResults.Errors.Invalid<string>("Activation token", null);
            }
            var NewEncryptedPWD = TrippleDES.Encrypt(model.NewPassword, AppSettingHelper.GetTrippleDesToken());

            accountConfirmation.Account.Password = NewEncryptedPWD;
            accountConfirmation.IsUsed = true;
            //await _repository.AuthToken.DisableAllSessions(accountConfirmation.AccountId);
            await _repository.SaveAsync();
            _ = _eventLogger.LogEvent(accountConfirmation.AccountId.ToString(), "User", "Password changed", new { });
            return ServiceResults.UpdatedSuccessfully("Password changed");
        }
        public async Task<ServiceResult<string>> ConfirmTokenChangePassword(ConfirmTokenChangePasswordRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Token))
                return ServiceResults.Errors.Required<string>("Token", null);
            var accountConfirmation = await _repository.AccountConfirmation.GetAccountConfirmationWithAccountByToken(model.Token);
            if (accountConfirmation == null)
                return ServiceResults.Errors.Invalid<string>("Confirm change password token", null);

            accountConfirmation.IsUsed = true;
            await _repository.SaveAsync();

            var Ac = new AccountConfirmation()
            {
                Type = EAccountConfirmationType.ForgetPasswordConfirmation,
                AccountId = accountConfirmation.Account.Id,
                Code = Guid.NewGuid().ToString(),
                IsUsed = false
            };

            _repository.AccountConfirmation.Create(Ac);
            await _repository.SaveAsync();
            //await _repository.AuthToken.DisableAllSessions(accountConfirmation.AccountId);
            _ = _eventLogger.LogEvent(accountConfirmation.AccountId.ToString(), "User", "Password changed", new { });
            return ServiceResults.GetSuccessfully(Ac.Code);
        }
        public async Task<ServiceResult<string>> LogOut(long accountId)
        {
            var account = await _repository.Account.GetByIdAsync(accountId);
            if (account == null)
                return ServiceResults.Empty<string>("Account doesn’t exist", null);

            var Deviceobj = await _repository.UserDevice.GetUserDevicesByAccountId(accountId);
            _repository.UserDevice.DeleteRange(Deviceobj);
            await _repository.SaveAsync();

            return ServiceResults.Empty<string>("You have successfully logged out", null);
        }
        
        public async Task<ServiceResult<AccountViewModel>> UpdateBannerImage(long accountId, UpdateBannerImageRequest model)
        {
            try
            {
                if (model == null)
                {
                    await _eventLogger.LogEvent("Update", "User", "updateAccountAsync", new { Error = "Account object sent from client is null" });
                    return ServiceResults.Errors.NotFound<AccountViewModel>("Account", null);
                }

                var account = await _repository.Account.GetByIdAsync(accountId);
                if (account == null)
                {
                    await _eventLogger.LogEvent("Id", "User", "updateAccountAsync", new { Error = "Account Not Found" });
                    return ServiceResults.Empty<AccountViewModel>("Account doesn’t exist", null);
                }

                if (model.IProfileBannerImage != null)
                {
                    //var byteArray = ConvertIFormFileToByteArrayAndResize(model.IProfileBannerImage);
                    //string uniquefilenameurl = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "-" + accountId;
                    //string extension = Path.GetExtension(model.IProfileBannerImage.FileName);
                    //if (string.IsNullOrEmpty(extension))
                    //{
                    //    extension = "";
                    //}
                    //var banner = await _fileManagementService.UploadImageBackblazeStorageBytesAsync(byteArray, uniquefilenameurl, model.IProfileBannerImage.ContentType, extension);
                    //if (banner.isSuccess)
                    //{
                    //    account.ProfileBannerImage = banner.response;
                    //}
                }

                _repository.Account.Update(account);
                await _repository.SaveAsync();

                return ServiceResults.TUpdatedSuccessfully("Profile", new AccountViewModel()
                {
                    Id = account.Id,
                    FirstName = account.FirstName,
                    Username = account.Username,
                    AccountType = account.AccountType.ToString(),
                    Bio = account.Bio,
                    DOB = account.DOB,
                    Email = account.Email,
                    Gender = account.EGender.ToString(),
                    ProfileBannerImage = account.ProfileBannerImage,
                    ProfileImage = account.ProfileImage
                });
            }
            catch (Exception ex)
            {
                await _eventLogger.LogEvent("Exception", "User", "updateAccountAsync", new { ex.Message });
                return ServiceResults.Errors.UnhandledError<AccountViewModel>(ex.Message, null);

            }

        }
        public async Task<ServiceResult<AccountViewModel>> UpdateProfileImage(long accountId, UpdateProfileImageRequest model)
        {
            try
            {
                if (model == null)
                {
                    await _eventLogger.LogEvent("Update", "User", "updateAccountAsync", new { Error = "Account object sent from client is null" });
                    return ServiceResults.Errors.NotFound<AccountViewModel>("Account", null);

                }

                var account = await _repository.Account.GetByIdAsync(accountId);
                if (account == null)
                {
                    await _eventLogger.LogEvent("Id", "User", "updateAccountAsync", new { Error = "Account Not Found" });
                    return ServiceResults.Empty<AccountViewModel>("Account doesn’t exist", null);
                }

                if (model.IProfileImage != null)
                {
                    //string uniquefilenameurl = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "-" + accountId;
                    //var byteArray = ConvertIFormFileToByteArrayAndResize(model.IProfileImage);
                    //string extension = Path.GetExtension(model.IProfileImage.FileName);
                    //if (string.IsNullOrEmpty(extension))
                    //{
                    //    extension = "";
                    //}
                    //var banner = await _fileManagementService.UploadImageBackblazeStorageBytesAsync(byteArray, uniquefilenameurl, model.IProfileImage.ContentType, extension);
                    //if (banner.isSuccess)
                    //{
                    //    account.ProfileImage = banner.response;
                    //}
                }

                _repository.Account.Update(account);
                await _repository.SaveAsync();

                return ServiceResults.TUpdatedSuccessfully("Profile", new AccountViewModel()
                {
                    Id = account.Id,
                    FirstName = account.FirstName,
                    Username = account.Username,
                    AccountType = account.AccountType.ToString(),
                    Bio = account.Bio,
                    DOB = account.DOB,
                    Email = account.Email,
                    Gender = account.EGender.ToString(),
                    ProfileBannerImage = account.ProfileBannerImage,
                    ProfileImage = account.ProfileImage
                });
            }
            catch (Exception ex)
            {
                await _eventLogger.LogEvent("Exception", "User", "updateAccountAsync", new { ex.Message });
                return ServiceResults.Errors.UnhandledError<AccountViewModel>(ex.Message, null);

            }

        }
        
        public async Task<ServiceResult<string>> VerifyEmailToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return ServiceResults.Errors.Required<string>("Token", null);
            var accountConfirmation = await _repository.AccountConfirmation.GetAccountConfirmationWithAccountByToken(token);
            if (accountConfirmation == null)
                return ServiceResults.Errors.Invalid<string>("Activation token", null);
            if (accountConfirmation.Type != EAccountConfirmationType.EmailVerification)
                return ServiceResults.Errors.Invalid<string>("Activation token", null);

            accountConfirmation.Account.EmailVerifiedAt = DateTime.UtcNow;
            accountConfirmation.Account.IsEmailVerified = true;
            accountConfirmation.IsUsed = true;
            await _repository.SaveAsync();
            return ServiceResults.UpdatedSuccessfully("Account is activated");
        }

        private string GenerateToken(long id, int type)
        {
            byte[] key = Encoding.ASCII.GetBytes(AppSettingHelper.GetJwtTokenSecret());

            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(TokenClaimKeys.Value, CryptoHelper.SymmetricEncryptString(AppSettingHelper.GetJwtValueSecret(), id.ToString())),
                    new Claim(TokenClaimKeys.Type, CryptoHelper.SymmetricEncryptString(AppSettingHelper.GetJwtValueSecret(), type.ToString())),
                }),

                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(700),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
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
                return null;
            }
        }
        private string GenerateRandomPassword(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                passwordBuilder.Append(validChars[index]);
            }

            return passwordBuilder.ToString();
        }
        public static string ExtractNumber(string input)
        {
            var match = Regex.Match(input, @"\d+(\.\d+)?");
            return match.Value;
        }
    }
}