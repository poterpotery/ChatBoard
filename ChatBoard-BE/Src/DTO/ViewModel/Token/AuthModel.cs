using System;
using DTO.ViewModel.Account;

namespace DTO.ViewModel.Token
{
	public class AuthModel
	{
		public AccountViewModel Account { get; set; }
		public string Token { get; set; }
	}
}

