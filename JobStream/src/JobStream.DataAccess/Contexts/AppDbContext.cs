using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Contexts
{
	public class AppDbContext : IdentityDbContext<AppUser>
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

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AboutUsConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateEducationConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateResumeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryFieldConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyAndCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new JobScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new JobTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new RubricForArticlesConfiguration());
            modelBuilder.ApplyConfiguration(new RubricForNewsConfiguration());
            modelBuilder.ApplyConfiguration(new SendMessageConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
