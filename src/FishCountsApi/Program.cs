var builder = WebApplication.CreateBuilder(args);

// Add the Startup class
var startup = new Startup(builder.Configuration);

// Configure services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware
startup.Configure(app, app.Environment);

app.Run();