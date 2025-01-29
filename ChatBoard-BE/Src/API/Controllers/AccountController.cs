using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Attributes;
using API.Utils;
using Common.Helpers;
using DTO.Enums;
using DTO.ViewModel;
using DTO.ViewModel.Account;
using DTO.ViewModel.Messages;
using DTO.ViewModel.Token;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Unit;
using Service.Models;

namespace API.Controllers
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceUnit _service;
        public AccountController(IServiceUnit service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<ServiceResult<AuthModel>>> SignUpAsync(SignUpRequestModel model)
        {
            try
            {
                var result = await _service.Account.SignUpAsync(model);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<ServiceResult<AuthModel>>> login(LoginRequestModel request)
        {
            try
            {
                var res = await _service.Account.LoginAsync(request);
                if (res.IsSuccess)
                    return Ok(res);
                else
                    return BadRequest(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpPost]
        [Route("UpdateAccount")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<bool>>> UpdateAccount(UpdateAccountRequestModel request)
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.UpdateAccountAsync(tokenModel.Id, request);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpGet]
        [Route("GetAccountById")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<bool>>> GetAccountById([FromQuery] long accountId)
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.GetAccountById(accountId);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpGet]
        [Route("Current")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<bool>>> Current()
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.Current(tokenModel.Id);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<List<UserDto>>>> getAllUsers([FromQuery] PaginationModel model)
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.GetUsersAsync(tokenModel.Id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));

            }
        }

        [HttpPost]
        [Route("SendMessage")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<bool>>> SendMessage(SendMessageRequest request)
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.SendMessageAsync(tokenModel.Id, request);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpPost]
        [Route("DeleteAccount")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<string>>> DeleteAccount()
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.DeleteAccount(tokenModel.Id);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }

        [HttpGet]
        [Route("GetMessages")]
        [CheckJwtToken(Allows = new[] { EAccountType.SuperAdmin, EAccountType.SubAdmin, EAccountType.User })]
        public async Task<ActionResult<ServiceResult<bool>>> GetMessages([FromQuery] GetMessagesRequest request)
        {
            try
            {
                var token = RequestUtil.GetToken(HttpContext);
                var tokenModel = _service.Account.ValidateToken(token);
                if (tokenModel == null)
                    return BadRequest(ServiceResults.Errors.UnhandledError<object>("token expired", null));
                var result = await _service.Account.GetMessagesAsync(tokenModel.Id, request);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }
    }
}