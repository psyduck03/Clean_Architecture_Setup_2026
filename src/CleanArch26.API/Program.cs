using System.Text;
using System.Threading.RateLimiting;
using CleanArch26.API.Exceptions;
using CleanArch26.API.Modules;
using CleanArch26.Application;
using CleanArch26.Application.Options;
using CleanArch26.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
    options.AddDocumentTransformer<CleanArch26.API.OpenApi.BearerSecuritySchemeTransformer>());
    
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors();

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("fixed", options =>
    {
        options.QueueLimit = 100;
        options.Window = TimeSpan.FromSeconds(1);
        options.PermitLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();

app.UseCors(cors =>
cors.AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(t => true));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.RegisterRoutes();
app.UseExceptionHandler();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();