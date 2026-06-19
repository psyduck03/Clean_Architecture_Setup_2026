using CleanArch26.Application.Services;
using CleanArch26.Domain.Users;
using FluentValidation;
using MediatR;

namespace CleanArch26.Application.Auth;
public sealed record LoginCommand(
    string UserName,
    string Password) : IRequest<LoginCommandResponse>;

public sealed record LoginCommandResponse(string Token, DateTime ExpiresAt);

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}

internal sealed class LoginCommandHandler(
    IAppUserRepository appUserRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await appUserRepository.FirstOrDefaultAsync(
            p => p.UserName == request.UserName, cancellationToken, isTrackingActive: false);

        if (user is null)
        {
            throw new Exception("Invalid user name or password.");
        }

        var isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new Exception("Invalid user name or password.");
        }

        var token = jwtProvider.CreateToken(user);
        return new LoginCommandResponse(token.AccessToken, token.ExpiresAt);
    }
}