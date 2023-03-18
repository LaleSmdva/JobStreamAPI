using JobStream.DataAccess.Repositories.Interfaces;

namespace JobStream.DataAccess.Repositories;

public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }
    IVacanciesRepository VacanciesRepository { get; }
    IAccountRepository AccountRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IJobTypeRepository JobTypeRepository { get; }
    IJobScheduleRepository JobScheduleRepository { get; }
    IRubricForNewsRepository RubricForNewsRepository { get; }
    IRubricForArticlesRepository RubricForArticlesRepository { get; }
    INewsRepository NewsRepository { get; }
    IArticleRepository ArticleRepository { get; }
    ICandidateResumeRepository CandidateResumeRepository { get; }
    ICompanyAndCategoryRepository CompanyAndCategoryRepository { get; }
    IAboutUsRepository AboutUsRepository { get; }
    ICandidateEducationRepository CandidateEducationRepository { get; }
    ICandidateResumeAndVacancyRepository CandidateResumeAndVacancyRepository { get; }
    ICategoryFieldRepository CategoryFieldRepository { get; }
    IApplicationRepository ApplicationRepository { get; }

    Task<int> SaveAsync();
}
