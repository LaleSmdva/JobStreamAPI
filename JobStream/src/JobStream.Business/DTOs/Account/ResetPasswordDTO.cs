using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account
{
	public class ResetPasswordDTO
	{
		public string? Newpassword { get; set; }
		public string? ConfirmPassword { get; set; }
	}
}
