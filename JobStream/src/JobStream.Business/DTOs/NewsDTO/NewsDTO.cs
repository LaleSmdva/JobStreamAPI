using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.NewsDTO
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? CreationTime { get; set; }
        public int RubricForNewsId { get; set; }
        public RubricForNews RubricForNews { get; }
        public IFormFile? Image { get; set; }
    }
}
