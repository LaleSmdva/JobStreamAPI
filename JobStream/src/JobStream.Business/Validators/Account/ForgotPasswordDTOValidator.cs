using FluentValidation;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class ForgotPasswordDTOValidator : AbstractValidator<ForgotPasswordDTO>
	{
		public ForgotPasswordDTOValidator()
		{
			RuleFor(e => e.Email).NotNull().WithMessage("Enter your email")
			.NotEmpty().WithMessage("Enter your email").EmailAddress().WithMessage("Enter custom email address");
		}
	}
}
