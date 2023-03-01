using JobStream.Core.Entities;
using JobStream.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.DTOs.ArticleDTO;

public class ArticleDTO
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public DateTime? PostedOn { get; set; }
	public int RubricForArticlesId { get; set; }
	//public RubricForArticles RubricForArticles { get; }
	public string? Image { get; set; }
}
