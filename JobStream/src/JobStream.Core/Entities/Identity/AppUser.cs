using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities.Identity
{
	public class AppUser:IdentityUser
	{
		public string? Fullname { get; set; }
		public string? Companyname { get; set; }
		public string? InfoCompany { get; set; } 
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpires { get; set; }
	}
}
