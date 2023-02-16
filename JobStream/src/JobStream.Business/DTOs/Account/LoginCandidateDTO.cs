using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account
{
	public class LoginCandidateDTO
	{
		public string? UsernameOrEmail { get; set; }
		public string? Password { get; set; }
	}
}
