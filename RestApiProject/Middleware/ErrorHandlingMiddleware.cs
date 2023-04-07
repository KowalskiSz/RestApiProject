using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestApiProject.Exceptions;
using System;
using System.Threading.Tasks;

namespace RestApiProject.Middleware
{
    //Middleware to catch exeptions - there is no need to define in each Controller a individual _logger methods 
    //and Try/Catch blocks
    public class ErrorHandlingMiddleware : IMiddleware //Ading the Interface to mark the class as middelware 
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        //Inject logger interface to constructor 
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        //Implement the InvokeAsync method from IMiddelware interface
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Calling next action to occure 
            try
            {
                await next.Invoke(context);
            }
            //if some exception thrown
            catch(NotFoundException notFoundExep)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundExep.Message);
            }

            catch(Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
