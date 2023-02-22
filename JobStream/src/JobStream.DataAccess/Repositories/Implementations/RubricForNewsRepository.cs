using JobStream.Core.Entities;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Implementations
{
    public class RubricForNewsRepository : Repository<RubricForNews>, IRubricForNewsRepository
    {
        public RubricForNewsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
