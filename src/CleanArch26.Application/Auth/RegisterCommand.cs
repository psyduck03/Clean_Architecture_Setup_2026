using CleanArch26.Application.Services;
using CleanArch26.Domain.Repository;
using CleanArch26.Domain.Users;
using FluentValidation;
using MediatR;

namespace CleanArch26.Application.Auth;
public sealed record RegisterCommand(
    string UserName,
    string Email,
    string Password) : IRequest<Guid>;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName).MinimumLength(3).WithMessage("User name must be at least 3 characters long.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("A valid email address is required.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

internal sealed class RegisterCommandHandler(
    IAppUserRepository appUserRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, Guid>
{
    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await appUserRepository.AnyAsync(
            p => p.UserName == request.UserName || p.Email == request.Email, cancellationToken);

        if (isUserExist)
        {
            throw new Exception("A user with the same user name or email already exists.");
        }

        AppUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = passwordHasher.Hash(request.Password)
        };

        appUserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}