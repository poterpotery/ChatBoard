using System;
using System.ComponentModel.DataAnnotations;
using DTO.Enums;
using Microsoft.AspNetCore.Http;

namespace DTO.ViewModel.Account
{
    public class UpdateAccountRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EGenderType Gender { get; set; }
    }
}