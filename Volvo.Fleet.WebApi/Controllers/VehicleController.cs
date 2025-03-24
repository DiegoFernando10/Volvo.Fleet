using Microsoft.AspNetCore.Mvc;
using Volvo.Fleet.Domain.Models.Vehicle;
using Volvo.Fleet.Domain.Services;

namespace Volvo.Fleet.WebApi.Controllers
{
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }
        [HttpGet]
        public async Task<List<VehicleDetail>> GetAll()
        {
            return await vehicleService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<VehicleDetail> GetById(string id)
        {
            return await vehicleService.GetById(id);
        }

        [HttpPost]
        public async Task<string> Create([FromBody] VehicleModel model)
        {
            if (model == null)
                throw new Exception("Model is null");

            return await vehicleService.CreateAsync(model);
        }

        [HttpPut("{id}")]
        public async Task Edit(string id, [FromBody] VehicleEditModel model)
        {
            if (model == null)
                throw new Exception("Model is null");

            await vehicleService.EditAsync(id, model);
        }

    }
}