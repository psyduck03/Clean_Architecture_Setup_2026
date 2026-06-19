using CleanArch26.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch26.Infrastructure.Configuration;
internal sealed class EmployeeConfigiration : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.OwnsOne(p => p.PersonelInformation, builder =>
        {
            builder.Property(i => i.IdentityNo).HasColumnName("IdentityNo");
            builder.Property(i => i.Phone).HasColumnName("Phone");
            builder.Property(i => i.Email).HasColumnName("Email");
        });

        builder.OwnsOne(p => p.Address, builder =>
        {
            builder.Property(i => i.Country).HasColumnName("Country");
            builder.Property(i => i.City).HasColumnName("City");
            builder.Property(i => i.ZipCode).HasColumnName("ZipCode");
            builder.Property(i => i.FullAddress).HasColumnName("FullAddress");
        });

        builder.Property(i => i.Salary).HasColumnType("Money");
	}
}
