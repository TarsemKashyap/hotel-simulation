using Api.Infra;
using Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service;
using System.Text;

namespace Api;
public static class ApiStartupExtensions
{





    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration _configRoot)
    {
        JwtSettings jwt = new JwtSettings();
        _configRoot.GetSection("JwtSettings").Bind(jwt);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(o =>
        {
            o.SaveToken = true; // revisit it
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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
            o.Events = new JwtBearerEvents()
            {

                OnAuthenticationFailed = OnAuthFailed,
                OnForbidden = OnForbidden,
                OnTokenValidated = onTokenValidated,
                OnChallenge = onChanllege,
                OnMessageReceived = onMessageRecieved,
            };

        });
    }

    private static Task onMessageRecieved(MessageReceivedContext arg)
    {
        return Task.CompletedTask;
    }

    private static Task onChanllege(JwtBearerChallengeContext arg)
    {
        return Task.CompletedTask;
    }

    private static Task onTokenValidated(TokenValidatedContext arg)
    {
        return Task.CompletedTask;
    }

    private static Task OnForbidden(ForbiddenContext arg)
    {
        return Task.CompletedTask;
    }

    private static Task OnAuthFailed(AuthenticationFailedContext context)
    {
        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        {
            context.Response.Headers.Add("token-expired", "true");
        }
        return Task.CompletedTask;
    }

    public static void AddCorsForApp(this IServiceCollection services, string policy)
    {
        services.AddCors(opt =>
              {
                  opt.AddPolicy(policy, builder =>
                  {

                      builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                  });
              });
    }


    public static void RegisterOptions(this IServiceCollection service, IConfiguration config)
    {
        service.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        service.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));
    }




    public static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
    }


}
