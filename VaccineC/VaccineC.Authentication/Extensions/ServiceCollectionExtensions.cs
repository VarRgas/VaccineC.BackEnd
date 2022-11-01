using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VaccineC.Authentication.Services;

namespace VaccineC.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(JwtSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

            return services;
        }
    }
}
