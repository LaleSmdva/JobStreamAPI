using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Contexts
{
	public class AppDbContextInitializer
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;

		public AppDbContextInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}
		public async Task SeedAsync()
		{
			foreach (var role in Enum.GetValues(typeof(UserRoles)))
			{
				if(!await _roleManager.RoleExistsAsync(role.ToString()))
				{
				    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
				}
			}
		}

		public async Task SeedUserAsync()
		{
			AppUser admin = new()
			{
				Fullname = _configuration["AdminSettings:Fullname"],
				UserName = _configuration["AdminSettings:UserName"],
				Email = _configuration["AdminSettings:Email"],
			};
			await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
			await _userManager.AddToRoleAsync(admin, UserRoles.Admin.ToString());
			
		}
	}
}
