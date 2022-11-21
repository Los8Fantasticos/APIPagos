namespace MinimalAPI_Pagos.Contracts.Services
{
    public interface IPagosService
    {
        /// <summary>
        /// Valida si está registrada la patente en la base de datos.
        /// </summary>
        /// <param name="PagosDTO">Propiedad Patente</param>
        /// <returns></returns>
        public Task<bool> ValidatePagos(string PagosDTO);
    }
}
