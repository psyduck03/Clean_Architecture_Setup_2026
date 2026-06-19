using CleanArch26.Domain.Abstraction;

namespace CleanArch26.Domain.Users;
public sealed class AppUser : Entity
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}