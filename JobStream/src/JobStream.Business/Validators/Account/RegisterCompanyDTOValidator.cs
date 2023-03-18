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
				.Length(3, 200).WithMessage("Company name can have at least 3 and a max of 200 characters");

			RuleFor(c => c.Email).NotNull().WithMessage("Enter email").NotEmpty().WithMessage("Enter email")
				.EmailAddress().WithMessage("Invalid email address");

			RuleFor(c => c.InfoCompany).MaximumLength(500).WithMessage("Max length is 500 characters");

			RuleFor(c => c.Password).NotNull().WithMessage("Enter password").NotEmpty().WithMessage("Enter password")
				.Length(6, 100).WithMessage("Password must be between 6 and 100 characters")
				.Matches("\\d").WithMessage("Password should contain at least 1 digit")
				.Matches("[A-Z]").WithMessage("Password should contain at least 1 uppercase")
				.Matches("[a-z]").WithMessage("Password should contain at least 1 lowercase")
				.Matches("[^A-Za-z0-9]").WithMessage("Password should contain at least 1 non-alphanumeric character.");

			RuleFor(c => c.ConfirmPassword).NotNull().WithMessage("Enter password").NotEmpty().WithMessage("Enter password")
				.Equal(c => c.Password).WithMessage("Password and confirmation password do not match");


		}
	}
}
