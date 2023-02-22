﻿using JobStream.Core.Entities;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Implementations
{
    public class CandidateResumeRepository : Repository<CandidateResume>,ICandidateResumeRepository
    {
        public CandidateResumeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
