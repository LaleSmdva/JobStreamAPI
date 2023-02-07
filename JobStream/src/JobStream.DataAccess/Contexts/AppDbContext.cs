using JobStream.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Company> Companies { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<CompanyAndCategory> CompaniesAndCategories { get; set; } = null!;
		public DbSet<Vacancy> Vacancies { get; set; } = null!;
		public DbSet<AboutUs> AboutUs { get; set; } = null!;

		public DbSet<Article> Articles { get; set; } = null!;

		public DbSet<News> News { get; set; } = null!;
		public DbSet<CandidateResume> Resumes { get; set; } = null!;

		public DbSet<RubricForNews> RubricForNews { get; set; } = null!;
		public DbSet<RubricForArticles> RubricForArticles { get; set; } = null!;
		//new

		public DbSet<JobType> JobTypes { get; set; } = null!;

		public DbSet<CategoryField> CategoryFields { get; set; } = null!;
		public DbSet<CandidateResume> CandidateResumes { get; set; } = null!;
		public DbSet<CandidateEducation> CandidateEducation { get; set; } = null!;
		//public DbSet<SendMessage> SendMessage { get; set; } = null!;
		//public DbSet<RulesForVacancy> RulesForVacancies { get; set; } = null!;
	}
}
