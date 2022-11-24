using Microsoft.EntityFrameworkCore;
using MinimalAPI_Pagos.Configurations;
using MinimalAPI_Pagos.Contracts.Repositories;
using MinimalAPI_Pagos.Contracts.Services;
using MinimalAPI_Pagos.Endpoints.Pagos;
using MinimalAPI_Pagos.Infrastructure;
using MinimalAPI_Pagos.Infrastructure.Repositories;
using MinimalAPI_Pagos.Receiver;
using MinimalAPI_Pagos.Services;

using RabbitMqService.Queues;
using RabbitMqService.RabbitMq;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder();


ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if (databaseContext != null)
    {
        //databaseContext.Database.EnsureCreated();
    }
    scope.ServiceProvider.GetService<PagoService>();
    scope.ServiceProvider.GetService<PagosEndpoint>()?.MapPagosEndpoint(app);
}


Configure(app, app.Environment);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{

    var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? builder.Configuration["ConnectionStrings"]?.ToString() ?? "";

    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, options =>
    {
        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
    }), ServiceLifetime.Singleton);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureLogger(builder);
    services.AddScoped<PagosEndpoint>();
    services.AddScoped<IPagosService, PagoService>();
    services.AddScoped<IPagosRepository, PagoRepository>();

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        settings.ExchangeName = configuration.GetValue<string>("AppSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("AppSettings:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<Pagos>();
    })
    .AddReceiver<PatenteReceiver<string>, string, PagoService>();

    //builder.Services.AddConfig<ApiReconocimientoConfig>(builder.Configuration, nameof(ApiReconocimientoConfig));
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        var context = app.ApplicationServices.GetService<ApplicationDbContext>();
        context?.Database?.Migrate();
    }

    app.UseHttpsRedirection();

    app.UseRouting();
}
