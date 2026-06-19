using CleanArch26.Domain.Users;

namespace CleanArch26.Application.Services;
public interface IJwtProvider
{
    JwtToken CreateToken(AppUser user);
}
public sealed record JwtToken(string AccessToken, DateTime ExpiresAt);