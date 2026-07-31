using System.Security.Claims;
using BffMvc.Extensions;
using BffMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BffMvc.Endpoints;

internal static partial class UserEndpoints
{
    internal static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("user", (ClaimsPrincipal principal) =>
        {
            var user = principal switch
            {
                { Identity.IsAuthenticated: true } => new User
                {
                    IsAuthenticated = true,
                    Name = principal.Identity.Name,
                    Claims = principal.Claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value }),
                },
                _ => new User
                {
                    IsAuthenticated = false,
                    Name = null
                }
            };

            return TypedResults.Ok(user);
        });

        return builder;
    }
}