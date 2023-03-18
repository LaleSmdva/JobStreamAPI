using AutoMapper;
using JobStream.Business.DTOs.ApplicationsDTO;
using JobStream.Business.DTOs.ApplyVacancyDTO;
using JobStream.Business.DTOs.CandidateResumeDTO;
using JobStream.Business.Exceptions;
using JobStream.Business.HelperServices.Interfaces;
using JobStream.Business.Services.Interfaces;
using JobStream.Business.Utilities;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace JobStream.Business.Services.Implementations
{

    public class CandidateResumeService : ICandidateResumeService
    {
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService1;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _env;
        private readonly ICandidateEducationRepository _candidateEducationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IVacanciesRepository _vacanciesRepository;
        private readonly ICandidateResumeAndVacancyRepository _candidateResumeAndVacancy;
        private readonly IApplicationRepository _applicationRepository;


        public CandidateResumeService(ICandidateResumeRepository candidateResumeRepository, IMapper mapper, IAccountService accountService1, UserManager<AppUser> userManager, IFileService fileService, IWebHostEnvironment env, ICandidateEducationRepository candidateEducationRepository, ICompanyRepository companyRepository, IVacanciesRepository vacanciesRepository, ICandidateResumeAndVacancyRepository candidateResumeAndVacancy, IApplicationRepository applicationRepository)
        {
            _candidateResumeRepository = candidateResumeRepository;
            _mapper = mapper;
            _accountService1 = accountService1;
            _userManager = userManager;
            _fileService = fileService;
            _env = env;
            _candidateEducationRepository = candidateEducationRepository;
            _companyRepository = companyRepository;
            _vacanciesRepository = vacanciesRepository;
            _candidateResumeAndVacancy = candidateResumeAndVacancy;
            _applicationRepository = applicationRepository;
        }


        public async Task<List<CandidateResumeDTO>> GetAllCandidatesResumesAsync()
        {
            List<CandidateResume> candidateResumes = await _candidateResumeRepository.GetAll()

                .Where(u => u.IsDeleted == false).ToListAsync();
            var list = _mapper.Map<List<CandidateResumeDTO>>(candidateResumes);
            return list;
        }

        public async Task<CandidateResumeDTO> GetCandidateResumeByUserId(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            CandidateResume resume = await _candidateResumeRepository.GetAll().FirstOrDefaultAsync(u => u.AppUserId == userId);


            CandidateResumeDTO result = _mapper.Map<CandidateResumeDTO>(resume);


            //result.AppUserId = user.Id;

            return result;
        }

        public async Task<CandidateResumeDTO> CandidateResumeDetails(int candidateId)
        {

            var resume = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (resume.IsDeleted == true)
                throw new NotFoundException("Resume not found");

            if (resume == null) throw new NotFoundException("Candidate not found");

            CandidateResume candidateResume = _candidateResumeRepository.GetAll()
               //.Include(e => e.CandidateEducation)
               .FirstOrDefault(e => e.Id == candidateId);
            var result = _mapper.Map<CandidateResumeDTO>(resume);

            return result;

        }

        /// Register zamani create olunur
        /// 
        //public async Task CreateCandidateResumeAsync(CandidateResumePostDTO entity)
        //{
        //    if (entity == null) throw new NullReferenceException("Candidate resume can't ne null");
        //    var user = await _userManager.FindByEmailAsync(entity.Email);
        //    if (user is null) throw new NotFoundException("User not found");


        //    if (entity.CV != null)
        //    {
        //        var fileName = await _fileService.CopyFileAsync(entity.CV, _env.WebRootPath, "images", "Resumes");

        //        CandidateResume resume = _mapper.Map<CandidateResume>(entity);
        //        resume.CV = fileName;
        //        resume.AppUserId = user.Id;

        //        //if (_candidateEducationRepository.GetAll().All(x => x.CandidateResumeId == resume.Id))
        //        //    throw new BadRequestException("Education is already created");

        //        CandidateEducation candidateEducation = new()
        //        {
        //            Major = entity.CandidateEducation.Major,
        //            Degree = entity.CandidateEducation.Degree,
        //            Institution = entity.CandidateEducation.Institution,
        //        };

        //        await _candidateResumeRepository.CreateAsync(resume);
        //    }
        //    await _candidateResumeRepository.SaveAsync();
        //}

        public async Task UpdateCandidateResumeAsync(int id, CandidateResumePutDTO candidateResume)
        {


            if (!candidateResume.ProfilePhoto.CheckFileFormat("image/"))
            {
                throw new FileFormatException("Please choose an image type.");
            }

            if (!candidateResume.CV.CheckFileFormat("application/pdf"))
            {
                throw new FileFormatException("Please choose a PDF file format.");
            }

            //var result = await _candidateResumeRepository.GetByIdAsync(id);

            var user = await _userManager.FindByEmailAsync(candidateResume.Email);
            user.Fullname = candidateResume.Fullname;
            user.PhoneNumber = candidateResume.Telephone.ToString();
            user.IsDeleted = false;

            var result = _mapper.Map<CandidateResume>(candidateResume);
            if (result.Email != candidateResume.Email)
            {
                throw new BadRequestException("You cannot change your email address.");
            }
            /////new
            //result.CandidateEducations = candidateResume.CandidateEducations;
            //
            result.ProfilePhoto = await _fileService.CopyFileAsync(candidateResume.ProfilePhoto, _env.WebRootPath, "images", "Resumes", "ProfilePicture");
            result.CV = await _fileService.CopyFileAsync(candidateResume.CV, _env.WebRootPath, "images", "Resumes", "CV");
            result.AppUser = user;
            result.AppUserId = user.Id;
            result.IsDeleted = false;

            //List<CandidateEducation> candidateEducations = new List<CandidateEducation>();

            //if (candidateResume.CandidateEducations!= null) { }
            //{
                //foreach (var education in candidateResume.CandidateEducations)
                //{
                //    var ed = new CandidateEducation()
                //    {
                //        EducationInfo = education,
                //        CandidateResumeId = result.Id
                //    };

                //    await _candidateEducationRepository.CreateAsync(ed);
                //}
                //await _candidateEducationRepository.SaveAsync();
            //}
            _candidateResumeRepository.Update(result);
            await _candidateResumeRepository.SaveAsync();

        }

        public async Task DeleteCandidateResume(int id)
        {
            var candidateResumes = _candidateResumeRepository.GetAll().ToList();
            if (candidateResumes.All(x => x.Id != id))
            {
                throw new NotFoundException("Not Found");
            }

            CandidateResume resume = await _candidateResumeRepository.GetByIdAsync(id);
            var user = _userManager.Users.FirstOrDefault(x => x.Id == resume.AppUserId);
            resume.IsDeleted = true;
            user.IsDeleted = true;
            await _candidateResumeRepository.SaveAsync();
        }

        public async Task ApplyVacancy(int candidateId, int companyId, int vacancyId, ApplyVacancyDTO applyVacancyDTO)
        {
            if ((!applyVacancyDTO.CV.CheckFileFormat("application/vnd.ms-word/")) && (!applyVacancyDTO.CV.CheckFileFormat("application/pdf"))
              && (!applyVacancyDTO.CV.CheckFileFormat("application/msword")) && (!applyVacancyDTO.CV.CheckFileFormat("text/plain")))
                throw new FileFormatException("Choose one of these file formats (doc, docx, pdf, text)");

            var fileName = await _fileService.CopyFileAsync(applyVacancyDTO.CV, _env.WebRootPath, "images", "Resumes", "CV");

            var candidate = await _candidateResumeRepository.GetByIdAsync(candidateId);
            if (candidate == null)
            {
                throw new NotFoundException("Candidate not found");
            }
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            Vacancy vacancy = await _vacanciesRepository.GetByIdAsync(vacancyId);

            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy not found");
            }
            if (vacancy.Applications == null)
            {
                vacancy.Applications = new List<Applications>();
            }

            var application = new Applications
            {
                CandidateResumeId = candidateId,
                VacancyId = vacancyId,
                CandidateResume = candidate,
                Vacancy = vacancy,
                CV = fileName,
            };

            await _applicationRepository.CreateAsync(application);
            await _applicationRepository.SaveAsync();

            CandidateResumeAndVacancy candidateResumeAndVacancy = new();
            candidateResumeAndVacancy.CandidateResumeId = application.Id;
            candidateResumeAndVacancy.VacancyId = vacancyId;
            candidateResumeAndVacancy.CandidateResume = candidate;
            candidateResumeAndVacancy.Vacancy = vacancy;


            await _candidateResumeAndVacancy.CreateAsync(candidateResumeAndVacancy);
            await _candidateResumeRepository.SaveAsync();

        }

        public async Task<List<ApplicationsResponseDTO>> ViewStatusOfAppliedJobs(int candidateId)
        {
            List<Applications> applications = _applicationRepository.GetAll().Where(c => c.CandidateResumeId == candidateId).ToList();
            var result = _mapper.Map<List<ApplicationsResponseDTO>>(applications);
            return result;
        }

        public async Task<List<ApplicationsResponseDTO>> GetAcceptedVacancies(int candidateId)
        {
            var acceptedVacancies = await _applicationRepository.GetAll().Where(u => u.CandidateResumeId == candidateId)
                .Where(r => r.IsAccepted == true).ToListAsync();
            var result = _mapper.Map<List<ApplicationsResponseDTO>>(acceptedVacancies);
            return result;

        }

        public async Task<List<ApplicationsResponseDTO>> GetRejectedVacancies(int candidateId)
        {
            var rejectedVacancies = await _applicationRepository.GetAll().Where(u => u.CandidateResumeId == candidateId)
                .Where(r => r.IsAccepted == false).ToListAsync();

            var result = _mapper.Map<List<ApplicationsResponseDTO>>(rejectedVacancies);
            return result;

        }
    }
}



