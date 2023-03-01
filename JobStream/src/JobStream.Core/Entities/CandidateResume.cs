using JobStream.Core.Entities.Identity;
using JobStream.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Core.Entities;



public class CandidateResume : IEntity
{
    public int Id { get; set; }
    public string? CV { get; set; }
    public string? Fullname { get; set; }
    public int? Telephone { get; set; }
    public string? Email { get; set; }
    public string? Location { get; set; }
    public string? DesiredPosition { get; set; }
    public string? AboutMe { get; set; }
    // ONE TO ONE  YAZMA
    //public int? CandidateEducationId { get; set; }
    //public CandidateEducation? CandidateEducation { get; set; } //major- Computer Science,Business Administration,
    //degree-Bachelor/Master of Business Administration (MBA),Bachelor/Master of Science (BS)
    //institution-Baku State University
    public string? LanguageSkills { get; set; }
    //[NotMapped]
    //public int? JobTypeId { get; set; }
    //public JobType? JobType { get; set; }

    //new 25
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public bool? IsDeleted { get; set; }

    public int? DesiredSalary { get; set; }
    public string? WorkExperience { get; set; }
    public string? Sertifications { get; set; }
    public string? LinkedinLink { get; set; }
    public string? GithubLink { get; set; }
    //new 26
    public string? ProfilePhoto { get; set; }
    ICollection<CandidateResumeAndVacancy>? CandidateResumesAndVacancies { get; set; }
    ICollection<Applications>? Applications { get; set; }
    //new 1
    //ICollection<CandidateEducation>? CandidateEducations { get; set; }
}

