using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using kamafi.auth.core.extensions;
using kamafi.auth.data;
using kamafi.auth.data.models;
using kamafi.auth.data.validators;
using kamafi.auth.data.extensions;
using kamafi.auth.services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddDataConfiguration(config)
    .AddControllers();

services.AddEndpointsApiExplorer();
services.AddScoped<ITokenRepository<User>, TokenRepository<User>>()
    .AddScoped<IAuthRepository, AuthRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ITenant, Tenant>()
    .AddSingleton<IValidator<TokenRequest>, TokenRequestValidator>()
    .AddSwaggerGen()
    .AddHttpContextAccessor();

services.AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config[Keys.JwtIssuer],
            ValidAudience = config[Keys.Audience],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Keys.JwtKey]))
        };
    });

services.AddMvcCore()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseAuthExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
