﻿namespace Volvo.Fleet.Domain.Models.Vehicle
{
    public class VehicleEditModel
    {
        public string Color { get; set; } = string.Empty;
        public void FormatData()
        {
            Color = Color.Trim();
        }
    }
}
