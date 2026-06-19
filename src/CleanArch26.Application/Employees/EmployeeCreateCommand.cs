using CleanArch26.Domain.Employees;
using CleanArch26.Domain.Repository;
using FluentValidation;
using MediatR;

namespace CleanArch26.Application.Employees;
public sealed record EmployeeCreateCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    decimal Salary,
    PersonelInformation PersonelInformation,
    Address? Address
    ) : IRequest<Employee>;

public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreateCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("First name must be at least 3 characters long.");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Last name must be at least 3 characters long.");
        RuleFor(x => x.DateOfBirth).LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0.");
        RuleFor(x => x.PersonelInformation.IdentityNo).MinimumLength(5).WithMessage("Identity number must be at least 5 characters long.");
    }
}
internal sealed class EmployeeCreateCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Employee>
{
  public async Task<Employee> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
  {
    var isEmployeeExist = await employeeRepository.AnyAsync(p => p.PersonelInformation.IdentityNo == request.PersonelInformation.IdentityNo, cancellationToken);
    
    if (isEmployeeExist)
    {
        throw new Exception("This employee already exists.");
    }

    Employee employee = new()
    {
      FirstName = request.FirstName,
      LastName = request.LastName,
      BirthOfDate = request.DateOfBirth,
      Salary = request.Salary,
      PersonelInformation = request.PersonelInformation,
      Address = request.Address
    };

    employeeRepository.Add(employee);
    
    await unitOfWork.SaveChangesAsync(cancellationToken);
    
    return await Task.FromResult(employee);
  }
}