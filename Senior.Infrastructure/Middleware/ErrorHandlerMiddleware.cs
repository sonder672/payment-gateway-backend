using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Senior.Infrastructure.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "Internal server error"
                };

                string response = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(response);
                context.Response.ContentType = "application/json";
            }
        }
    }
}