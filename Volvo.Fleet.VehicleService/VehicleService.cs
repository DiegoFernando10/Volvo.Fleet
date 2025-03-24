using Microsoft.EntityFrameworkCore;
using Volvo.Fleet.Domain.Models.Vehicle;
using Volvo.Fleet.Domain.Services;
using Volvo.Fleet.Entities;
using Volvo.Fleet.Entities.Vehicle;
using Volvo.Fleet.Extentions;
using Volvo.Fleet.FluentValidation;

namespace Volvo.Fleet.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<VehicleDetail>> GetAll()
        {
            var res = await unitOfWork
                .VehicleRepository
                .AsQueryable()
                .Select(v => new VehicleDetail
                {
                    ChassisId = v.ChassisId,
                    Color = v.Color,
                    NumberOfPassengers = v.NumberOfPassengers,
                    Type = v.TypeEnum.GetEnumDescriptionValue()
                })
                .ToListAsync();

            return res;
        }

        public async Task<VehicleDetail> GetById(string id)
        {
            return await unitOfWork
                .VehicleRepository
                .Where(v => v.ChassisId == id)
                .Select(v => new VehicleDetail
                {
                    ChassisId = v.ChassisId,
                    Color = v.Color,
                    NumberOfPassengers = v.NumberOfPassengers,
                    Type = v.TypeEnum.GetEnumDescriptionValue()
                })
                .FirstOrDefaultAsync() ?? throw new Exception("Vehice Not Found");
        }

        public async Task<string> CreateAsync(VehicleModel model)
        {
            #region  Validations

            model.FormatData();

            await new Validators.VehicleCreationValidator().Run(model);

            var chassisId = $"{model.ChassisSeries}-{model.ChassisNumber}";

            if (await unitOfWork
                .VehicleRepository
                .Where(v => v.ChassisId == chassisId)
                .AnyAsync())
                throw new Exception("Vehice Not Found");

            #endregion

            var vehicle = new Vehicle(model.Type)
            {
                ChassisId = chassisId,
                Color = model.Color,
            };

            unitOfWork.VehicleRepository.Add(vehicle);

            await unitOfWork.SaveAsync();

            return vehicle.ChassisId;
        }

        public async Task EditAsync(string id, VehicleEditModel model)
        {
            model.FormatData();

            await new Validators.VehicleEditionValidator().Run(model);

            var vehicle = await unitOfWork
                .VehicleRepository
                .Where(v => v.ChassisId == id)
                .FirstOrDefaultAsync() ?? throw new Exception("Vehice Not Found");

            vehicle.Color = model.Color;

            await unitOfWork.SaveAsync();
        }
    }
}