using JobStream.Business.DTOs.Account;
using JobStream.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace JobStream.Business.DTOs.CandidateResumeDTO;

public class CandidateResumeDTO
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

    //public string? AppUserId { get; set; }
    //public AppUser? AppUser { get; set; }
    //public bool? IsDeleted { get; set; }
    public string? ProfilePhoto { get; set; }

    public int? DesiredSalary { get; set; }
    public string? WorkExperience { get; set; }
    public string? Sertifications { get; set; }
    public string? LinkedinLink { get; set; }
    public string? GithubLink { get; set; }
    //public bool? IsDeleted { get; set; }
}
