using Volvo.Fleet.Domain.Enums;

namespace Volvo.Fleet.Entities.Vehicle
{
    public class Vehicle
    {
        public string ChassisId { get; set; } = string.Empty;
        public VehicleTypeEnum TypeEnum { get; set; }
        public int NumberOfPassengers { get; set; }
        public string Color { get; set; } = string.Empty;

        public Vehicle(VehicleTypeEnum typeEnum)
        {
            TypeEnum = typeEnum;

            switch (typeEnum)
            {
                case VehicleTypeEnum.Car:
                    NumberOfPassengers = 4;
                    break;
                case VehicleTypeEnum.Truck:
                    NumberOfPassengers = 1;
                    break;
                case VehicleTypeEnum.Bus:
                    NumberOfPassengers = 42;
                    break;
            }
        }
    }
}
