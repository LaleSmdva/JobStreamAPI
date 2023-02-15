using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account
{
	public class RegisterCompanyDTO
	{
		public string? Companyname { get; set; }
		public string? Email { get; set; }
		public string? InfoCompany { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
	}
}
