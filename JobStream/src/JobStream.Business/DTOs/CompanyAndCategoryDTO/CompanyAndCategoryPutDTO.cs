using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.CompanyAndCategoryDTO
{
    public class CompanyAndCategoryPutDTO
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        //public Company Company { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
