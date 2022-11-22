using System.Diagnostics.CodeAnalysis;
using MinimalAPI_Pagos.Contracts.Services;
using MinimalAPI_Pagos.Endpoints.Errors;
using MinimalAPI_Pagos.Models.ApplicationModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace MinimalAPI_Pagos.Endpoints.Pagos
{
    [ExcludeFromCodeCoverage]
    public class PagosEndpoint
    {
        private readonly IPagosService _pagosService;
        private readonly ILogger<PagosEndpoint> _logger;
        public PagosEndpoint(IPagosService pagoService, ILoggerFactory logger)
        {
            _pagosService = pagoService;
            _logger = logger.CreateLogger<PagosEndpoint>();
        }

        public async Task MapPagosEndpoint(WebApplication app)
        {
            _ = app.MapGet(
               "/api/pagos",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Buscando cantidad de pagos facturados");
                       int result = await _pagosService.CountPagos();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint Pagos.");
                       throw;
                   }
               })
           .WithTags("Pagos")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<PagosModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapGet(
               "/api/totalFacturado",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Buscando total facturado");
                       double result = await _pagosService.TotalRecaudado();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint Pagos.");
                       throw;
                   }
               })
           .WithTags("Pagos")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<PagosModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
