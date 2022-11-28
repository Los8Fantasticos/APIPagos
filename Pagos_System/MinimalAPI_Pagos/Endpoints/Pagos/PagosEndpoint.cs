using System.Diagnostics.CodeAnalysis;
using MinimalAPI_Pagos.Contracts.Services;
using MinimalAPI_Pagos.Endpoints.Errors;
using MinimalAPI_Pagos.Models.ApplicationModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

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
               "/api/pagos/getPrice",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Obteniendo el ultimo precio");
                       double result = await _pagosService.GetLastPrice();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error XD");
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
               "/api/pagos/totalFacturado",
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

            _ = app.MapPost(
               "/api/pagos/modificarPrecioPeaje",
               async ([FromBody] double nuevoPrecio) =>
               {
                   try
                   {
                       _logger.LogInformation("Se busca el total de multas realizadas");
                       double result = await _pagosService.ModificarPrecioPeaje(nuevoPrecio);
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint Multa.");
                       throw;
                   }
               })
           .WithTags("Pagos")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<double>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }
    }
}
