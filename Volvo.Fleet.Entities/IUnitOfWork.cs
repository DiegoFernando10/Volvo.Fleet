using Volvo.Fleet.Domain.Interfaces;

namespace Volvo.Fleet.Entities
{
    public interface IUnitOfWork
    {
        IGenericRepository<Vehicle.Vehicle> VehicleRepository { get; }

        Task SaveAsync();
    }
}
