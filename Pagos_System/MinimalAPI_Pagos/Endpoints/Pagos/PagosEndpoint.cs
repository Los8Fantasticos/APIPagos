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
        private readonly IPagosService _patenteService;
        private readonly ILogger<PagosEndpoint> _logger;
        public PagosEndpoint(IPagosService pagoService, ILoggerFactory logger)
        {
            _patenteService = pagoService;
            _logger = logger.CreateLogger<PagosEndpoint>();
        }

        public async Task MapPagosEndpoint(WebApplication app)
        {
            _ = app.MapPost(
               "/api/pagos/{pagos}",
               async (PagosModel pagosModel) =>
               {
                   try
                   {
                       pagosModel.Active = true;
                       _logger.LogInformation("test");
                       string result = "asdasd";
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
