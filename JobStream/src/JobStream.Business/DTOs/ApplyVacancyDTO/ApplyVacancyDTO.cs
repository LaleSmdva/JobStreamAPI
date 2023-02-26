using JobStream.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.ApplyVacancyDTO
{
    public class ApplyVacancyDTO
    {
        public IFormFile CV { get; set; }
    }
}
