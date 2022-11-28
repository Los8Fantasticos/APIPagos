namespace MinimalAPI_Pagos.Contracts.Services
{
    public interface IPagosService
    {
        public Task<int> CountPagos();
        public Task<double> GetLastPrice();
        public Task<double> TotalRecaudado();
        public Task<double> ModificarPrecioPeaje(double nuevoValor);
    }
}
