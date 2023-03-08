using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Api.Infra;
public static class ServiceCollectionExtension
{
    public static void AddJwt(this IServiceCollection services, JwtSettings jwt)
    {
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true; // revisit it
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.AccessTokenSecret)),
                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}