using CleanArch26.Application.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch26.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IQueryable<EmployeeGetAllQueryResponse>> GetAllEmployees(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new EmployeeGetAllQuery(), cancellationToken);
        return response;
    }
}
