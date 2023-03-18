using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Implementations;
using JobStream.DataAccess.Repositories.Interfaces;
namespace JobStream.DataAccess.Repositories;
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private ICompanyRepository? _companyRepository;
    private IVacanciesRepository? _vacanciesRepository;
    private IAccountRepository? _accountRepository;
    private ICategoryRepository? _categoryRepository;
    private IJobTypeRepository? _jobTypeRepository;
    private IJobScheduleRepository? _jobScheduleRepository;
    private IArticleRepository? _articleRepository;
    private INewsRepository? _newsRepository;
    private IRubricForNewsRepository? _rubricForNewsRepository;
    private IRubricForArticlesRepository? _rubricForArticlesRepository;
    private ICandidateResumeRepository? _candidateResumeRepository;
    private ICompanyAndCategoryRepository? _companyAndCategoryRepository;
    private ICandidateEducationRepository? _candidateEducationRepository;
    private IAboutUsRepository? _aboutUsRepository;
    private ICandidateResumeAndVacancyRepository? _candidateResumeAndVacancyRepository;
    private ICategoryFieldRepository? _categoryFieldRepository;
    private IApplicationRepository? _applicationRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public ICompanyRepository CompanyRepository => _companyRepository = _companyRepository ?? new CompanyRepository(_context);
    public IVacanciesRepository VacanciesRepository => _vacanciesRepository = _vacanciesRepository ?? new VacanciesRepository(_context);
    public IAccountRepository AccountRepository => _accountRepository = _accountRepository ?? new AccountRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
    public IJobTypeRepository JobTypeRepository => _jobTypeRepository = _jobTypeRepository ?? new JobTypeRepository(_context);
    public IJobScheduleRepository JobScheduleRepository => _jobScheduleRepository = _jobScheduleRepository ?? new JobScheduleRepository(_context);
    public IRubricForNewsRepository RubricForNewsRepository => _rubricForNewsRepository = _rubricForNewsRepository ?? new RubricForNewsRepository(_context);
    public IRubricForArticlesRepository RubricForArticlesRepository => _rubricForArticlesRepository = _rubricForArticlesRepository ?? new RubricForArticlesRepository(_context);
    public INewsRepository NewsRepository => _newsRepository = _newsRepository ?? new NewsRepository(_context);
    public IArticleRepository ArticleRepository => _articleRepository = _articleRepository ?? new ArticleRepository(_context);
    public ICandidateResumeRepository CandidateResumeRepository => _candidateResumeRepository = _candidateResumeRepository ?? new CandidateResumeRepository(_context);
    public ICompanyAndCategoryRepository CompanyAndCategoryRepository => _companyAndCategoryRepository = _companyAndCategoryRepository ?? new CompanyAndCategoryRepository(_context);
    public IAboutUsRepository AboutUsRepository => _aboutUsRepository = _aboutUsRepository ?? new AboutUsRepository(_context);
    public ICandidateEducationRepository CandidateEducationRepository => _candidateEducationRepository = _candidateEducationRepository ?? new CandidateEducationRepository(_context);
    public ICandidateResumeAndVacancyRepository CandidateResumeAndVacancyRepository => _candidateResumeAndVacancyRepository = _candidateResumeAndVacancyRepository ?? new CandidateResumeAndVacancyRepository(_context);
    public ICategoryFieldRepository CategoryFieldRepository => _categoryFieldRepository = _categoryFieldRepository ?? new CategoryFieldRepository(_context);
    public IApplicationRepository ApplicationRepository => _applicationRepository = _applicationRepository ?? new ApplicationRepository(_context);
    public async Task<int> SaveAsync()
    {
       return await _context.SaveChangesAsync(); 
    }
}
