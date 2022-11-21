using MinimalAPI_Pagos.Contracts.Repositories;
using MinimalAPI_Pagos.Contracts.Services;

namespace MinimalAPI_Pagos.Services
{
    public class PagoService : IPagosService
    {
        private readonly IPagosRepository _pagosRepository;
        public PagoService(IPagosRepository pagosRepository)
        {
            _pagosRepository = pagosRepository;
        }

        public Task<bool> ValidatePagos(string PagosDTO)
        {
            throw new NotImplementedException();
        }
    }
}
