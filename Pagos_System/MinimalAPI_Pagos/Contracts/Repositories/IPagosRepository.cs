using MinimalAPI_Pagos.Models.ApplicationModel;

namespace MinimalAPI_Pagos.Contracts.Repositories
{
    public interface IPagosRepository
    {
        public Task<PagosModel> GetPagos(string PagosDTO);
        public Task CountPagos(bool IsRecognition, PagosModel LastTrafic = null);
        public Task<PagosModel> GetLastTrafic();
    }
}

