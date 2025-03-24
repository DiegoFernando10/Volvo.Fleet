using Volvo.Fleet.Domain.Interfaces;
using Volvo.Fleet.Entities;
using Volvo.Fleet.Entities.Vehicle;

namespace Volvo.Fleet.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VolvoDbContext context;

        public UnitOfWork(VolvoDbContext context)
        {
            this.context = context;
        }

        private IGenericRepository<Vehicle> vehicleRepository;
        public IGenericRepository<Vehicle> VehicleRepository
        {
            get
            {
                return this.vehicleRepository ??= new GenericRepository<VolvoDbContext, Vehicle>(context);
            }
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
