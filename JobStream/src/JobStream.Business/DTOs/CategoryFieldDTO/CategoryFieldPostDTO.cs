﻿using JobStream.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.CategoryFieldDTO
{
    public class CategoryFieldPostDTO
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
