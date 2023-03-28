using Api;
using Database;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

const string _policyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCorsForApp(_policyName);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<AccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<InstructorAccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClassSessionDtoValidator>();

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

builder.Services.RegisterAppServices();
builder.Services.RegisterMapster();



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
app.Services.CreateScope();
//app.MapControllers();;
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());

if (builder.Configuration.GetValue<bool>("RunMigration"))
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<HotelDbContext>();

        // Here is the migration executed
        dbContext.Database.Migrate();
    }
}
app.Run();

