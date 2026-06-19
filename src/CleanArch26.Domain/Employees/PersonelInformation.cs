namespace CleanArch26.Domain.Employees;
public sealed record PersonelInformation
{
    public string IdentityNo { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}