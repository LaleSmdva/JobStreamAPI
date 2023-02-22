﻿using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.CandidateResumeDTO
{
    public class CandidateResumePutDTO
    {
        public int Id { get; set; }

        //public IFormFile? CV { get; set; }
        public string? Fullname { get; set; }
        public int? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? DesiredPosition { get; set; }
        public string? AboutMe { get; set; }
        // ONE TO ONE 
        public CandidateEducation? CandidateEducation { get; set; } //major- Computer Science,Business Administration,
                                                                    //degree-Bachelor/Master of Business Administration (MBA),Bachelor/Master of Science (BS)
                                                                    //institution-Baku State University
        public string? LanguageSkills { get; set; }
        //[NotMapped]
        public int? JobTypeId { get; set; }
        public JobType? JobType { get; set; }

        //public int AppUserId { get; set; }
        //public AppUser AppUser { get; set; }
        public bool? IsDeleted { get; set; }

        public int? DesiredSalary { get; set; }
        public string? WorkExperience { get; set; }
        public string? Sertifications { get; set; }
        public string? LinkedinLink { get; set; }
        public string? GithubLink { get; set; }
    }
}
