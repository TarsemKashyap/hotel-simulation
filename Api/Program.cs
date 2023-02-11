using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Api.Infra;
using Api;
var builder = WebApplication.CreateBuilder(args);

ApiStartup startup = new ApiStartup(builder.Configuration);
startup.RegisterServices(builder.Services);
var app = builder.Build();
startup.Configure(app, builder.Environment);

app.Run();
