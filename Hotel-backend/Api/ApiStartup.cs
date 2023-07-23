using Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service;
using System.Text;
using Mapster;
using MapsterMapper;
using System.Reflection;

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
        service.Configure<Smtp>(config.GetSection("smtp"));
        service.Configure<AdminConfig>(config.GetSection("AdminConfig"));
        service.Configure<PaymentConfig>(config.GetSection("PaymentConfig"));
    }

    public static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IClassSessionService, ClassSessionService>();
        services.AddScoped<IStudentSignupTempService, StudentSignupTempService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IMonthService, MonthServices>();
        services.AddScoped<IStudentClassMappingService, StudentClassMappingService>();
        services.AddScoped<IStudentRolesMappingService, StudentRolesMappingService>();
        services.AddScoped<IStudentGroupMappingService, StudentGroupMappingService>();
        services.AddScoped<IRoomAllocationService, RoomAllocationService>();
        services.AddScoped<IAttributeDecisionService, AttributeDecisionService>();
        services.AddScoped<IPriceDecisionService, PriceDecisionService>();
        services.AddScoped<ICalculationServices, CalculationServices>();
        services.AddScoped<IGoalReportService, GoalReportService>();
        services.AddScoped<IPerformanceReportService, PerformanceReportService>();
        services.AddScoped<IIncomeReportService, IncomeReportService>();
        services.AddScoped<IBalanceReportService, BalanceReportService>();


    }
    public static void RegisterMapster(this IServiceCollection services)
    {
        //var config = new TypeAdapterConfig();
        // Or
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        config.Scan(Assembly.GetAssembly(typeof(MappingProfile)));

        // register the mapper as Singleton service for my application
        var mapperConfig = new MapsterMapper.Mapper(config);
        services.AddSingleton(mapperConfig);
        services.AddTransient<IMapper, ServiceMapper>();
    }
}