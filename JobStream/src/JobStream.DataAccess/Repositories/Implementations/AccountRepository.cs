using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;

namespace JobStream.DataAccess.Repositories.Implementations;

public class AccountRepository : Repository<AppUser>, IAccountRepository
{
	public AccountRepository(AppDbContext context) : base(context)
	{
	}
}
