using Volvo.Fleet.Domain.Models.Vehicle;

namespace Volvo.Fleet.Domain.Services
{
    public interface IVehicleService
    {
        public Task<List<VehicleDetail>> GetAll();
        public Task<VehicleDetail> GetById(string id);
        public Task<string> CreateAsync(VehicleModel model);
        public Task EditAsync(string id, VehicleEditModel model);
    }
}
