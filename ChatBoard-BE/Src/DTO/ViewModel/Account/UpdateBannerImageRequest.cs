using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTO.ViewModel.Account
{
	public class UpdateBannerImageRequest
	{
        public string ProfileBannerImage { get; set; }
        [Required(ErrorMessage = "Banner Image is required.")]
        public IFormFile IProfileBannerImage { get; set; }
    }
}

