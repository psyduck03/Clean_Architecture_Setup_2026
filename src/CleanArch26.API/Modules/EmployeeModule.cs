using CleanArch26.Application.Employees;
using MediatR;

namespace CleanArch26.API.Modules;
public static class EmployeeModule
{
    public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/employees").WithTags("Employees");
        group.MapPost(string.Empty,
            async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response != null ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces(StatusCodes.Status200OK);
    }
}
