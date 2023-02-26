using AutoMapper;
using JobStream.Business.DTOs.CompanyDTO;
using JobStream.Business.DTOs.VacanciesDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{
	public class VacanciesMapper : Profile
	{
		public VacanciesMapper()
		{
			CreateMap<Vacancy, VacanciesDTO>()/*.ForMember(d=>d.CompanyName,opt=>opt.MapFrom(src=>src.Company.Name)).ReverseMap()*/;
			CreateMap<Vacancy, VacanciesPostDTO>().ReverseMap();
			CreateMap<Vacancy, VacanciesPutDTO>().ReverseMap();
		}
	}
}
