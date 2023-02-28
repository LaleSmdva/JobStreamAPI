using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A = JobStream.Business.DTOs.NewsDTO;
namespace JobStream.Business.DTOs.RubricForNewsDTO
{
    public class RubricForNewsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<A.NewsDTO>? News { get; set; }
    }
}
