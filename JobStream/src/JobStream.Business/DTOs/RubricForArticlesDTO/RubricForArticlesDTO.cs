using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A=JobStream.Business.DTOs.ArticleDTO;

namespace JobStream.Business.DTOs.RubricForArticlesDTO
{
    public class RubricForArticlesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<A.ArticleDTO>? Articles { get; set; }
    }
}
