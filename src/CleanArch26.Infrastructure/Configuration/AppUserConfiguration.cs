using CleanArch26.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch26.Infrastructure.Configuration;
internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(p => p.UserName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(255).IsRequired();
        builder.Property(p => p.PasswordHash).IsRequired();

        builder.HasIndex(p => p.UserName).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();
    }
}