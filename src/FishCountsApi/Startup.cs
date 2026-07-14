using FishCountsApi.EcologyApi.Clients;
using FishCountsApi.Handlers;
using Microsoft.OpenApi.Models;
using System.Reflection;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddScoped<IGetFishCountsHandler, GetFishCountsHandler>();
        services.AddSingleton<IEcologyApiClient>(sp =>
        {
            var baseUrl = _configuration["EcologyApiBaseUrl"] ?? "";
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            return new EcologyApiClient(httpClient);
        });

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Fish Counts API", Version = "v1" });
            options.SupportNonNullableReferenceTypes();
            options.DescribeAllParametersInCamelCase();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
