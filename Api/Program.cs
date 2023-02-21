using Api;
var builder = WebApplication.CreateBuilder(args);

ApiStartup startup = new ApiStartup(builder.Configuration);
startup.RegisterServices(builder.Services);
var app = builder.Build();
startup.Configure(app, builder.Environment);

app.Run();
