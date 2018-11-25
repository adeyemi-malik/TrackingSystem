using Mints.BLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Middleware
{
    public class ApiKeyValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiClientRepository apiClientRepository)
        {
            if(context.Request.Path.Value.Contains("/api/v1/location/save", StringComparison.OrdinalIgnoreCase) || !context.Request.Path.Value.Contains("/api/", StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context);
            }
            else
            {
                if (!context.Request.Headers.Keys.Any(h => h.Equals("ApiKey", StringComparison.OrdinalIgnoreCase)))
                {
                    context.Response.Headers["WWW-Authenticate"] = $" ApiKey error=\"missing\", error_description=\"apikey is not present\"";
                    context.Response.StatusCode = 400;              
                    await context.Response.WriteAsync("Api Key is missing");
                    return;
                }
                else
                {
                    var apiKey = context.Request.Headers.FirstOrDefault(h => h.Key.Equals("ApiKey", StringComparison.OrdinalIgnoreCase)).Value;
                    if (!await apiClientRepository.Exists(apiKey))
                    {
                        context.Response.Headers["WWW-Authenticate"] = $" ApiKey error=\"invalid_apiKey\", error_description=\"apikey is not recognize\"";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Invalid Api Key");
                        return;
                    }
                }
                await _next.Invoke(context);
            }

        }

    }

    #region ExtensionMethod
    public static class ApiKeyValidatorExtension
    {
        public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyValidatorMiddleware>();
            return app;
        }
    }
    #endregion
}
