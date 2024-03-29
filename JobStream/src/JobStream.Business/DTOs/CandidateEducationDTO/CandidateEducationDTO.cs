﻿using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.CandidateEducationDTO
{
    public class CandidateEducationDTO
    {
        public int Id { get; set; }
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public string? Institution { get; set; }
        public int? CandidateResumeId { get; set; }
        //public CandidateResume? CandidateResume { get; set; }
    }
}
