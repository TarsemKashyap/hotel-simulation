using Api.Infra;
using Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service;

namespace Api;
public class ApiStartup
{
    public IConfiguration _configRoot
    {
        get;
    }

    public ApiStartup(IConfiguration configuration)
    {
        _configRoot = configuration;
    }
    private readonly string _policyName = "CorsPolicy";
    public void RegisterServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddValidatorsFromAssemblyContaining<AccountDto>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        RegisterOptions(services);
        services.AddDbContext<HotelDbContext>(options =>
        {
            options.UseMySQL(_configRoot.GetConnectionString("DbConn"));
        });
        services.AddIdentity<AppUser, AppUserRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 5;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<HotelDbContext>()
        .AddDefaultTokenProviders();
        // services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
        JwtSettings jwt = new JwtSettings();
        _configRoot.GetSection("JwtSettings").Bind(jwt);
        services.AddSingleton<JwtSettings>(jwt);
        services.AddJwt(jwt);
        services.AddCors(opt =>
        {
            opt.AddPolicy(_policyName, builder =>
            {

                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        RegisterService(services);
    }


    private void RegisterOptions(IServiceCollection service)
    {
        service.Configure<JwtSettings>(this._configRoot.GetSection("JwtSettings"));
        service.Configure<ConnectionStrings>(this._configRoot.GetSection("ConnectionStrings"));
    }


    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();
        app.UseCors(_policyName);
        app.MapControllers();

    }

    private void RegisterService(IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
    }


}
