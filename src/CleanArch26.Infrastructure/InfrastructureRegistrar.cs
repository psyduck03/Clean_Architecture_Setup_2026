using CleanArch26.Application.Options;
using CleanArch26.Application.Services;
using CleanArch26.Domain.Employees;
using CleanArch26.Domain.Repository;
using CleanArch26.Domain.Users;
using CleanArch26.Infrastructure.Context;
using CleanArch26.Infrastructure.Repositories;
using CleanArch26.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch26.Infrastructure;
public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;
            options.UseSqlServer(connectionString);
        });

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}