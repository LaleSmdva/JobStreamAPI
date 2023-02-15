using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account
{
	public class RegisterCandidateDTO
	{
		public string? Fullname { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
	}
}

