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
        services.AddIdentity<AppUser, AppUserRole>().AddEntityFrameworkStores<HotelDbContext>().AddDefaultTokenProviders();
        services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));
        Jwt jwt = new Jwt();
        _configRoot.GetSection("jwt").Bind(jwt);
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
        service.Configure<Jwt>(this._configRoot.GetSection("jwt"));
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
        services.AddTransient<IAccountService, AccountService>();
    }
}
