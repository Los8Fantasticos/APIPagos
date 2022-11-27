using MinimalAPI_Pagos.Models.ApplicationModel;

namespace MinimalAPI_Pagos.Contracts.Repositories
{
    public interface IPagosRepository
    {
        public Task Insert(PagosModel pagosModel);
        public Task<int> CountPagos();
        public Task<double> GetAllPagos();
    }
}

