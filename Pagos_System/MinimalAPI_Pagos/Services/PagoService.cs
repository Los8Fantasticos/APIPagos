using MinimalAPI_Pagos.Contracts.Repositories;
using MinimalAPI_Pagos.Contracts.Services;
using MinimalAPI_Pagos.Models.ApplicationModel;

using RabbitMqService.Abstractions;

namespace MinimalAPI_Pagos.Services
{
    public class PagoService : IPagosService, IMessageReceiver<string>
    {
        private readonly IPagosRepository _pagosRepository;
        private readonly ILogger logger;
        public PagoService(IPagosRepository pagosRepository, ILogger<PagoService> logger)
        {
            _pagosRepository = pagosRepository;
            this.logger = logger;
        }

        public async Task<int> CountPagos() => await _pagosRepository.CountPagos();

        public async Task ReceiveAsync(string message, CancellationToken cancellationToken)
        {
            logger.LogInformation("Mensaje recibido para multar una patente");

            PagosModel pagosModel = new PagosModel();
            pagosModel.Patente = message;
            //traer monto de settings
            pagosModel.Monto = 100;

            await _pagosRepository.Insert(pagosModel);

            logger.LogInformation($"Se realizo el pago de la patente: {message}.");
        }

        public async Task<double> TotalRecaudado()
        {
            var listapagos = await _pagosRepository.GetAllPagos();
            double total = 0;
            foreach (var item in listapagos)
            {
                total += item.Monto;
            }
            return total;           
        }
    }
}
