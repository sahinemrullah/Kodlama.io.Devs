using Core.Security.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Security.Stores;
using Security.Entities;
using Security;
using Core.Security;
using Security.Hashing;
using Core.Security.Encryption;
using Security.Accessors.UserAccessor;

namespace Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSecurityServices();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["TokenOptions:Audience"],
                ValidIssuer = configuration["TokenOptions:Issuer"],
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(configuration["TokenOptions:SecurityKey"]),
            };
        });
        services.AddAuthorization();
        services.AddScoped<IPasswordHasher<Developer>, CorePasswordHasher>();

        services.AddIdentityCore<Developer>(
            options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
            })
            .AddSignInManager()
            .AddUserStore<DeveloperStore>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserAccessor, UserAccessor>();
        return services;
    }
}