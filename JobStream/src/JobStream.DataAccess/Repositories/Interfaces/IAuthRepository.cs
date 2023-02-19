using JobStream.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Interfaces;

public interface IAuthRepository : IRepository<AppUser>
{
}
