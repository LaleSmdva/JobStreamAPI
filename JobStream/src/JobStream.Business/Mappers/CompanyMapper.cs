using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
	public class CompanyMapper:Profile
	{
		public CompanyMapper()
		{
			CreateMap<Company,CompanyDTO>().ReverseMap();
			CreateMap<Company,CompanyPutDTO>().ReverseMap();
			CreateMap<Company,TestCompanyPutDTO>().ReverseMap();
			CreateMap<Company,CompanyPostDTO>().ReverseMap();
		}
	}
}
