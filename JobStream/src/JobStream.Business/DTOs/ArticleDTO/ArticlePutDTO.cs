using JobStream.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.ArticleDTO;

public class ArticlePutDTO
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public int RubricForArticlesId { get; set; }
    //public RubricForArticles RubricForArticles { get; }
    public IFormFile? Image { get; set; }
}
