using FluentValidation;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class LoginCandidateDTOValidator:AbstractValidator<LoginDTO>
	{
		public LoginCandidateDTOValidator()
		{
			RuleFor(c => c.UsernameOrEmail)
			.NotNull().WithMessage("Username/Email address cannot be null")
			.NotEmpty().WithMessage("Username/Email address cannot be empty")
			.Custom((name, context) =>
			{
				if (name.Contains("@"))
				{
					bool isValid = new EmailAddressAttribute().IsValid(name);
					if (!isValid)
					{
						context.AddFailure("Enter valid email address");
					}
				}
			});

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
