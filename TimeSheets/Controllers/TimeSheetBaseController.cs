using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheets.Infrastructure.Validation;

namespace TimeSheets.Controllers
{
	/// <summary>
	/// Базовый кастомный контроллер с подключенной валидацией
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class TimeSheetBaseController : Controller
	{
		public override Task OnActionExecutionAsync(
			ActionExecutingContext context,
			ActionExecutionDelegate next)
		{
			if (!ModelState.IsValid)
			{
				var result = new ErrorModel { Errors = new Dictionary<string, string>() };
				
				foreach (var value in context.ModelState.Values)
				{
					if (value.ValidationState == ModelValidationState.Invalid)
					{
						var propertyValue = value.GetType().GetProperties()
							.FirstOrDefault(s => s.Name == "Key")
							?.GetValue(value, null)?.ToString().ToLower();

						if (propertyValue != null)
						{
							if (!result.Errors.ContainsKey(propertyValue))
							{
								result.Errors.Add(propertyValue, value.Errors.FirstOrDefault()
									.ErrorMessage ?? ValidationsMessages.InvalidValue);
							}
						}
					}
				}

				context.Result = new BadRequestObjectResult(result);

				return Task.CompletedTask;
			}

			return base.OnActionExecutionAsync(context, next);
		}

	}
}
