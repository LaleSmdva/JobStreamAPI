using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.Business.Validators.Account
{
	public class ValidateRolesModelAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ActionArguments.ContainsKey("roles"))
			{
				context.Result = new BadRequestObjectResult(new ErrorResponse
				{
					Message = "Invalid roles data",
					Details = "Roles data is required and cannot be null."
				});
				return;
			}

			var roles = context.ActionArguments["roles"] as List<string>;

			if (roles == null || roles.Count == 0)
			{
				context.Result = new BadRequestObjectResult(new ErrorResponse
				{
					Message = "Invalid roles data",
					Details = "At least one role must be provided."
				});
				return;
			}
		}
	}

	public class ErrorResponse
	{
		public string Message { get; set; }
		public string Details { get; set; }
	}
}
