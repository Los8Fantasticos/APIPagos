using Microsoft.EntityFrameworkCore;
using MinimalAPI_Pagos.Configurations;
using MinimalAPI_Pagos.Contracts.Repositories;
using MinimalAPI_Pagos.Contracts.Services;
using MinimalAPI_Pagos.Endpoints.Pagos;
using MinimalAPI_Pagos.Infrastructure;
using MinimalAPI_Pagos.Infrastructure.Repositories;
using MinimalAPI_Pagos.Services;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder();

var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? builder.Configuration["ConnectionStrings"]?.ToString() ?? "";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, options =>
{
    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
}), ServiceLifetime.Singleton);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogger(builder);
builder.Services.AddScoped<PagosEndpoint>();
builder.Services.AddScoped<IPagosService, PagoService>();
builder.Services.AddScoped<IPagosRepository, PagoRepository>();

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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var context = app.Services.GetService<ApplicationDbContext>();
    context?.Database?.Migrate();
}

app.UseHttpsRedirection();

app.Run();

