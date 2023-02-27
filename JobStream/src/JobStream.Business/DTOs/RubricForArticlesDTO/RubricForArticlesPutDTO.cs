using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.RubricForArticlesDTO
{
    public class RubricForArticlesPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Article>? Articles { get; set; }
    }
}
