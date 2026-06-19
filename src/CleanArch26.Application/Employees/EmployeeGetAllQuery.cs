using CleanArch26.Domain.Abstraction;
using CleanArch26.Domain.Employees;
using MediatR;

namespace CleanArch26.Application.Employees;
public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;
public sealed class EmployeeGetAllQueryResponse : EntityDTO
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public string IdentityNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = employeeRepository.GetAll()
            .Select(e => new EmployeeGetAllQueryResponse
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthOfDate = e.BirthOfDate,
                Salary = e.Salary,
                IdentityNo = e.PersonelInformation.IdentityNo,
                CreateAt = e.CreateAt,
                UpdateAt = e.UpdateAt,
                IsDeleted = e.IsDeleted,
                DeleteAt = e.DeleteAt
            })
            .AsQueryable();
        return Task.FromResult(response);
    }
}
