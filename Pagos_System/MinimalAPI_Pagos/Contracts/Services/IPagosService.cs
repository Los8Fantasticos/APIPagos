namespace MinimalAPI_Pagos.Contracts.Services
{
    public interface IPagosService
    {
        public Task<int> CountPagos();
        public Task<double> TotalRecaudado();
    }
}
