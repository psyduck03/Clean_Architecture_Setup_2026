
using CleanArch26.Domain.Employees;
using CleanArch26.Domain.Repository;
using CleanArch26.Infrastructure.Context;

namespace CleanArch26.Infrastructure.Repositories;
internal sealed class EmployeeRepository : Repository<Employee, ApplicationDbContext>, IEmployeeRepository
{
  public EmployeeRepository(ApplicationDbContext context) : base(context)
  {
  }
}
