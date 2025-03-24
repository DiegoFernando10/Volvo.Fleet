using Volvo.Fleet.Domain.Enums;

namespace Volvo.Fleet.Domain.Models.Vehicle
{
    public class VehicleDetail
    {
        public string ChassisId { get; set; } = string.Empty;
        public string Type { get; set; }
        public int NumberOfPassengers { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
