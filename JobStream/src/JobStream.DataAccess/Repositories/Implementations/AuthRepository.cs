using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Implementations
{
	public class AuthRepository : Repository<AppUser>, IAuthRepository
	{
		public AuthRepository(AppDbContext context) : base(context)
		{
		}
	}
}
