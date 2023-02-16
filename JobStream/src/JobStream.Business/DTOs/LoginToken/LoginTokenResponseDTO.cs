using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.LoginToken
{
	public class LoginTokenResponseDTO
	{
		public string? Token { get; set; }
		public DateTime Expires { get; set; }
		public string? Username { get; set; }
	}
}
