using FluentValidation;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class LoginCompanyDTOValidator:AbstractValidator<LoginDTO>
	{
		public LoginCompanyDTOValidator()
		{
			//RuleFor(c => c.Email).EmailAddress().WithMessage("Insert a valid email address");
			RuleFor(ue => ue.UsernameOrEmail).NotNull().NotEmpty().WithMessage("Username/Email field can't be empty");
			RuleFor(c => c.Password)
				.NotNull().WithMessage("Enter your password")
				.NotEmpty().WithMessage("Enter your password")
				.Length(6, 100).WithMessage("Password must be between 6 and 100 characters")
				.Matches("\\d").WithMessage("Password should contain at least 1 digit")
				.Matches("[A-Z]").WithMessage("Password should contain at least 1 uppercase")
				.Matches("[a-z]").WithMessage("Password should contain at least 1 lowercase")
				.Matches("[^A-Za-z0-9]").WithMessage("Password should contain at least 1 non-alphanumeric character.");
		}
	}
}
