using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestApiProject.Middleware
{
    public class TimeRequestMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger<TimeRequestMiddleware> _logger;

        public TimeRequestMiddleware(ILogger<TimeRequestMiddleware> logger)
        {
            _stopwatch = new Stopwatch();

            //Loger implementation - to log the message in the txt file
            _logger = logger;
        }

        //Main middleware fun
        public async Task  InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //Middleware logic 
            _stopwatch.Start();
            await next.Invoke(context); //Calling rest of the request - counting the time on this method
            _stopwatch.Stop();

            var timeresult = _stopwatch.ElapsedMilliseconds;

            if(timeresult / 1000 > 4)
            {
                string message = $"Request {context.Request.Method} at {context.Request.Path} took {timeresult} ms";

                _logger.LogInformation(message);
            }

        }
    }
}
