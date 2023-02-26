using AutoMapper;
using JobStream.Business.DTOs.AboutUsDTO;
using JobStream.Business.DTOs.InvitationDTO;
using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Mappers
{

    public class InvitationMapper : Profile
    {
        public InvitationMapper()
        {
            CreateMap<Invitation, InvitationDTO>().ReverseMap();
            CreateMap<Invitation, InvitationPostDTO>().ReverseMap();
            CreateMap<Invitation, InvitationPutDTO>().ReverseMap();

        }
    }
}
