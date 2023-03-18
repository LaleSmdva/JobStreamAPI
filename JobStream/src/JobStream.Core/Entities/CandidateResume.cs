using JobStream.Core.Entities.Identity;
using JobStream.Core.Interfaces;
using Newtonsoft.Json;

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
    public string? LanguageSkills { get; set; }
    //[NotMapped]
    //public int? JobTypeId { get; set; }
    //public JobType? JobType { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public bool? IsDeleted { get; set; }

    public int? DesiredSalary { get; set; }
    public string? WorkExperience { get; set; }
    public string? Sertifications { get; set; }
    public string? LinkedinLink { get; set; }
    public string? GithubLink { get; set; }
    public string? ProfilePhoto { get; set; }
    ICollection<CandidateResumeAndVacancy>? CandidateResumesAndVacancies { get; set; }
    ICollection<Applications>? Applications { get; set; }
    public string? CandidateEducation { get; set; }
    //[JsonIgnore]
    //ICollection<CandidateEducation>? CandidateEducations { get; set; }
}

