using MinimalAPI_Pagos.Contracts.Repositories;

namespace MinimalAPI_Pagos.Infrastructure.Repositories
{
    public class PagoRepository : IPagosRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PagoRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
