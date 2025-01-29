using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTO.ViewModel.Account
{
	public class UpdateProfileImageRequest
	{
        public string ProfileImage { get; set; }
        public IFormFile IProfileImage { get; set; }
    }
}