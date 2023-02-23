using AutoMapper;
using JobStream.Business.DTOs.Account;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = JobStream.Business.DTOs.Account;

namespace JobStream.Business.Mappers;

public class AppUserMapper : Profile
{
	public AppUserMapper()
	{
		CreateMap<AppUser, AppUserDTO>().ReverseMap();
		CreateMap<AppUser, C.CompanyDTO>().ReverseMap();
		CreateMap<AppUser, CandidateDTO>().ReverseMap();
	}
}
