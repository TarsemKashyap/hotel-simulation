using Api;
using Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

const string _policyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<AccountDto>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DbConn"));
});

builder.Services.AddIdentity<AppUser, AppUserRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<HotelDbContext>();
//  .AddDefaultTokenProviders();
// services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.RegisterOptions(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsForApp(_policyName);
builder.Services.RegisterAppServices();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors(_policyName);
//app.MapControllers();;
app.UseEndpoints(endpoints => endpoints.MapControllers());
//app.UseMiddleware<JwtMiddleware>();
app.Run();
