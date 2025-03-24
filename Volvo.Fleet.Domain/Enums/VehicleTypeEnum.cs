using System.ComponentModel;

namespace Volvo.Fleet.Domain.Enums
{
    public enum VehicleTypeEnum
    {
        [Description("Bus")]
        Bus = 0,
        [Description("Truck")]
        Truck = 1,
        [Description("Car")]
        Car = 2
    }
}
