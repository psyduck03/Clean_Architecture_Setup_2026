using CleanArch26.Application.Auth;
using MediatR;

namespace CleanArch26.API.Modules;
public static class AuthModule
{
    public static void RegisterAuthRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/auth").WithTags("Auth");

        group.MapPost("/register",
            async (ISender sender, RegisterCommand request, CancellationToken cancellationToken) =>
            {
                var userId = await sender.Send(request, cancellationToken);
                return Results.Ok(new { Id = userId });
            })
            .Produces(StatusCodes.Status200OK);

        group.MapPost("/login",
            async (ISender sender, LoginCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return Results.Ok(response);
            })
            .Produces<LoginCommandResponse>(StatusCodes.Status200OK);
    }
}