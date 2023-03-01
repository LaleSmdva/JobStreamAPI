using AutoMapper;
using JobStream.Business.DTOs.Account;
using JobStream.Business.Exceptions;
using JobStream.Business.Services.Interfaces;
using JobStream.Core.Entities;
using JobStream.Core.Entities.Identity;
using JobStream.Core.Enums;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;
using ArgumentNullException = JobStream.Business.Exceptions.ArgumentNullException;
using C = JobStream.Business.DTOs.Account;

namespace JobStream.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICandidateResumeRepository _candidateResumeRepository;
        private readonly ICompanyRepository _companyRepository;

        public AccountService(UserManager<AppUser> userManager, IAccountRepository accountRepository, IMapper mapper, RoleManager<IdentityRole> roleManager, ICandidateResumeRepository candidateResumeRepository, ICompanyRepository companyRepository)
        {
            _userManager = userManager;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _roleManager = roleManager;
            _candidateResumeRepository = candidateResumeRepository;
            _companyRepository = companyRepository;
        }


        public async Task<List<CandidateDTO>> GetAllCandidateAccounts()
        {

            var accounts = _accountRepository.GetAll().Where(a => a.Companyname == null).ToList();
            if (accounts is null) throw new NotFoundException("No candidate account exists");

            List<AppUser> candidates = new List<AppUser>();

            foreach (var user in accounts)
            {
                var isCandidate = await _userManager.IsInRoleAsync(user, "Candidate");
                if (isCandidate)
                    candidates.Add(user);

            }
            var result = _mapper.Map<List<CandidateDTO>>(candidates);
            return result;
        }

        public async Task<List<C.CompanyDTO>> GetAllCompanyAccounts()
        {
            var accounts = _accountRepository.GetAll().Where(a => a.Companyname != null).ToList();
            if (accounts is null) throw new NotFoundException("No company account exists");
            List<AppUser> companies = new List<AppUser>();

            foreach (var user in accounts)
            {
                var isCompany = await _userManager.IsInRoleAsync(user, "Company");
                if (isCompany)
                    companies.Add(user);

            }
            var result = _mapper.Map<List<C.CompanyDTO>>(companies);
            return result;
        }

        public async Task<CandidateDTO> GetCandidateAccountByUsernameAsync(string userName)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) throw new NotFoundException("User not found");
            var isCandidate = await _userManager.IsInRoleAsync(user, "Candidate");
            if (!isCandidate) throw new NotFoundException("User not found");
            var response = _mapper.Map<CandidateDTO>(user);
            return response;
        }
        public async Task<C.CompanyDTO> GetCompanyAccountByCompanyNameAsync(string companyName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Companyname == companyName);
            if (user == null) throw new NotFoundException("Company not found");
            var isCompany = await _userManager.IsInRoleAsync(user, "Company");
            if (!isCompany) throw new NotFoundException("Company not found");
            var response = _mapper.Map<C.CompanyDTO>(user);
            return response;
        }

        public async Task RegisterCandidate(RegisterCandidateDTO registerCandidate)
        {

            AppUser candidate = new()
            {
                Fullname = registerCandidate.Fullname,
                UserName = registerCandidate.Username,
                Email = registerCandidate.Email,
            };

            if (await _userManager.Users.AnyAsync(u => u.UserName == candidate.UserName))
            {
                throw new DuplicateUserNameException("User with the same username already exists");
            }
            if (await _userManager.Users.AnyAsync(u => u.Email == candidate.Email))
            {
                throw new DuplicateEmailException("User with the same email address already exists");
            }
            //
            var candidateResume = new CandidateResume
            {
                Fullname = registerCandidate.Fullname,
                Email = registerCandidate.Email,
                AppUser=candidate,
                AppUserId=candidate.Id
            };

            candidate.CandidateResume = candidateResume;
            //


            var identityResult = await _userManager.CreateAsync(candidate, registerCandidate.Password);


            if (!identityResult.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var error in identityResult.Errors)
                {
                    errorMessages.Add(error.Description);
                }
                throw new CreateUserFailedException($"{errorMessages}");
            }


            var result = await _userManager.AddToRoleAsync(candidate, UserRoles.Candidate.ToString());
            if (!result.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.Description);
                }
                throw new CreateRoleFailedException($"{errorMessages}");
            }
            

            await _candidateResumeRepository.SaveAsync();
            //////// 
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(candidate);
          

        }

        public async Task<List<object>> GetAllRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = new List<object>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                roles.Add(new { Identity = user.UserName, Roles = userRoles });
            }

            return roles;
        }

        public async Task<bool> CreateRoleAsync(string userName, List<string> roles)
        {

            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("Username is required");
            var User = await _userManager.FindByNameAsync(userName);

            List<string> allRoles = new();

            foreach (var rolee in Enum.GetValues(typeof(UserRoles)))
            {
                allRoles.Add(rolee.ToString());
            }

            foreach (var role in roles)
            {
                if (!allRoles.Contains(role))
                {
                    throw new NotFoundException("There is no such a role.Choose among: Candidate, Company, Moderator, Admin");
                }

                var newRole = new IdentityRole
                {
                    Name = role,
                };
                await _roleManager.CreateAsync(newRole);
                var identityResult = await _userManager.AddToRoleAsync(User, role);

            }
            return true;
        }


        public async Task<bool> UpdateRoleAsync(string userName, List<string> newRoles, List<string> deletedRoles)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("Username is required");
            var user = await _userManager.FindByNameAsync(userName);

            List<string> allRoles = new();

            foreach (var rolee in Enum.GetValues(typeof(UserRoles)))
            {
                allRoles.Add(rolee.ToString());
            }

            if (user != null)
            {
                foreach (var newRole in newRoles)
                {
                    if (!allRoles.Contains(newRole))
                    {
                        throw new NotFoundException("There is no such a role");
                    }
                    
                    if (await _roleManager.RoleExistsAsync(newRole))
                    {
                        var newRolee = new IdentityRole()
                        {
                            Name = newRole,
                        };
                        await _roleManager.CreateAsync(newRolee);
                        await _userManager.AddToRoleAsync(user, newRole);
                    }
                }

                foreach (var deletedRole in deletedRoles)
                {
                    if (!allRoles.Contains(deletedRole))
                    {
                        throw new NotFoundException("There is no such a role");
                    }
                   
                    if (await _roleManager.RoleExistsAsync(deletedRole))
                    {
                        var deletedRolee = new IdentityRole()
                        {
                            Name = deletedRole,
                        };
                        await _userManager.RemoveFromRoleAsync(user, deletedRole);
                    }
                }
            }
            return true;
        }


        public async Task<List<string>> GetRolesById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }
            throw new NotFoundException("User not found");
        }
   

        public async Task RegisterCompany(RegisterCompanyDTO registerCompany)
        {
            AppUser company = new()
            {
                UserName = Regex.Replace(registerCompany.Companyname, @"[^a-zA-Z0-9]+", "_").ToLower() + Guid.NewGuid().ToString().Substring(0, 8),/*registerCompany.Companyname.Replace(" ", "").ToLower() + Guid.NewGuid().ToString().Substring(0, 8),*/
                Companyname = registerCompany.Companyname,
                Email = registerCompany.Email,
                InfoCompany = registerCompany.InfoCompany,
            };
            if (await _userManager.Users.AnyAsync(u => u.UserName == company.Companyname))
            {
                throw new DuplicateUserNameException("User with the same company name already exists");
            }
            if (await _userManager.Users.AnyAsync(u => u.Email == company.Email))
            {
                throw new DuplicateEmailException("User with the same email address already exists");
            }
            //new
            var Company = new Company
            {
                Name = registerCompany.Companyname,
                Email = registerCompany.Email,
                AboutCompany = registerCompany.InfoCompany,
                Location = "-",
               

            };
            company.Company = Company;
            //new

            var identityResult = await _userManager.CreateAsync(company, registerCompany.Password);
            if (!identityResult.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var error in identityResult.Errors)
                {
                    errorMessages.Add(error.Description);
                }
                //throw new CreateUserFailedException($"{errorMessages}");
                throw new CreateUserFailedException(string.Join(", ", errorMessages));
                
            }


            var result = await _userManager.AddToRoleAsync(company, UserRoles.Company.ToString());
            if (!result.Succeeded)
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.Description);
                }
                throw new CreateRoleFailedException($"{errorMessages}");
            }
            await _companyRepository.SaveAsync();
        }


        ///////    new ////
        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(company.Id);
        //var callbackUrl = Url.Link("ConfirmEmailRoute", new { userId = company.Id, code = code });
        //await _userManager.SendEmailAsync(company.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");



    }
}

