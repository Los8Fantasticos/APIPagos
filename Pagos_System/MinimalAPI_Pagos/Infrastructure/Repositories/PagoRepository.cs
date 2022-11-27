using Microsoft.EntityFrameworkCore;

using MinimalAPI_Pagos.Contracts.Repositories;
using MinimalAPI_Pagos.Models.ApplicationModel;

namespace MinimalAPI_Pagos.Infrastructure.Repositories
{
    public class PagoRepository : IPagosRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PagoRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> CountPagos() => await _applicationDbContext.Factura.CountAsync();

        //Contar todos los montos
        public async Task<double> GetAllPagos() => await _applicationDbContext.Factura.SumAsync(x => x.Monto);
        public async Task Insert(PagosModel pagos)
        {
            _applicationDbContext?.Factura?.Add(pagos);
            await _applicationDbContext?.SaveChangesAsync();
        }
    }
}
