using FluentValidation;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class ResetPasswordDTOValidator : AbstractValidator<ResetPasswordDTO>
	{
		public ResetPasswordDTOValidator()
		{
			RuleFor(c => c.Newpassword)
				.NotNull().WithMessage("Enter your password")
				.NotEmpty().WithMessage("Enter your password")
				.Length(6, 100).WithMessage("Password must be between 6 and 100 characters")
				.Matches("\\d").WithMessage("Password should contain at least 1 digit")
				.Matches("[A-Z]").WithMessage("Password should contain at least 1 uppercase")
				.Matches("[a-z]").WithMessage("Password should contain at least 1 lowercase")
				.Matches("[^A-Za-z0-9]").WithMessage("Password should contain at least 1 non-alphanumeric character.");
			RuleFor(c => c.ConfirmPassword)
				.NotNull().WithMessage("Enter your password")
				.NotEmpty().WithMessage("Enter your password")
			    .Equal(c => c.Newpassword).WithMessage("Password and confirmation password do not match");
		}
	}
}
