using Volvo.Fleet.Domain.Enums;

namespace Volvo.Fleet.Domain.Models.Vehicle
{
    public class VehicleModel
    {
        public string ChassisSeries { get; set; }

        public uint ChassisNumber { get; set; }

        public VehicleTypeEnum Type { get; set; }

        public string Color { get; set; }

        public void FormatData()
        {
            ChassisSeries = ChassisSeries.Trim();
            Color = Color.Trim();
        }
    }
}
