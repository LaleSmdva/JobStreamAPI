using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.Account;

public class AppUserDTO
{
	//public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? CompanyName { get; set; }
	public string? FullName { get; set; }
	public string? Email { get; set; }
    public int? CandidateResumeId { get; set; }
}
