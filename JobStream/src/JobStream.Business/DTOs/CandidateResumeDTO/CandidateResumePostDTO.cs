using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = JobStream.Business.DTOs.CandidateEducationDTO;

namespace JobStream.Business.DTOs.CandidateResumeDTO
{
    public class CandidateResumePostDTO
    {
        public IFormFile? CV { get; set; }
        public string? Fullname { get; set; }
        public int? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public string? DesiredPosition { get; set; }
        public string? AboutMe { get; set; }
        //public E.CandidateEducationPostDTO? CandidateEducation { get; set; }
        public string? LanguageSkills { get; set; }

        //public int AppUserId { get; set; }
        //public AppUser AppUser { get; set; }
        //public bool? IsDeleted { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public int? DesiredSalary { get; set; }
        public string? WorkExperience { get; set; }
        public string? Sertifications { get; set; }
        public string? LinkedinLink { get; set; }
        public string? GithubLink { get; set; }
    }
}
