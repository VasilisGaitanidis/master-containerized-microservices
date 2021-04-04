using System;
using System.Linq;
using Application.Exceptions;
using Cart.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cart.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment hostingEnvironment, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (_hostingEnvironment.IsDevelopment())
            {
                return;
            }

            if (context.Exception is CartDomainException domainException)
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more errors occurred.",
                    Detail = "One or more errors occurred. Please refer to the errors property for additional details."
                };

                problemDetails.Errors.Add("DomainErrors", new[] { domainException.Message });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception is ValidationAppException validationAppException)
            {
                var problemDetails = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred.",
                    Detail = "One or more validation errors occurred. Please refer to the errors property for additional details."
                };

                problemDetails.Errors.Add("ApplicationValidationErrors", new[] { context.Exception.Message });

                if (validationAppException.InnerException is ValidationException validationException)
                {
                    problemDetails.Errors.Add("CommandValidationErrors", validationException.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception is EntityNotFoundException entityNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Detail = entityNotFoundException.Message
                };

                context.Result = new NotFoundObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}