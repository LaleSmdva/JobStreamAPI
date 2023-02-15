﻿using FluentValidation;
using JobStream.Business.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class RegisterCompanyDTOValidator : AbstractValidator<RegisterCompanyDTO>
	{
		public RegisterCompanyDTOValidator()
		{
			RuleFor(c => c.Companyname).NotNull().WithMessage("Enter the name of company").NotEmpty().WithMessage("Enter the name of company")
				.Length(3, 200).WithMessage("Fullname can have at least 3 and a max of 200 characters");

			RuleFor(c => c.Email).NotNull().WithMessage("Enter email").NotEmpty().WithMessage("Enter email")
				.EmailAddress().WithMessage("Invalid email address");

			RuleFor(c => c.InfoCompany).MaximumLength(300).WithMessage("Max length is 300 characters");

			RuleFor(c => c.Password).NotNull().WithMessage("Enter password").NotEmpty().WithMessage("Enter password")
				.Length(6, 100).WithMessage("Password must be between 6 and 100 characters");

			RuleFor(c => c.ConfirmPassword).NotNull().WithMessage("Enter password").NotEmpty().WithMessage("Enter password")
				.Equal(c => c.Password).WithMessage("Password and confirmation password do not match");
		}
	}
}