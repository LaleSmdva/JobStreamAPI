﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.AboutUsDTO
{
    public class AboutUsDTO
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Image { get; set; }
        public string? Info { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkedinLink { get; set; }
    }
}
