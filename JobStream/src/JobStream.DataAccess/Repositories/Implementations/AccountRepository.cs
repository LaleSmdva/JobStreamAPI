using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Implementations;

public class AccountRepository : Repository<AppUser>, IAccountRepository
{
	public AccountRepository(AppDbContext context) : base(context)
	{
	}
}
